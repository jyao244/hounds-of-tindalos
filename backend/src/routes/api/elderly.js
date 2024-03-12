import express from 'express'
import { verify } from './verifyToken'
import Elderly from '../../db/elderly'
import User from '../../db/user'
import Location from '../../db/location'

const router = express.Router()

//add a device
router.post('/add', async (req, res) => {
  const userId = req.body.userId
  const imei = req.body.imei

  const dbUser = await User.findById(userId)

  if (dbUser) {
    const elderly = await Elderly.create({
      imei: imei,
      wear: false,
      saveZone: false,
      powerWarning: false,
    })

    const newElderlyId = elderly._id
    const elderlys = dbUser.elderlys
    elderlys.push(newElderlyId)

    await User.updateOne(
      { _id: userId },
      {
        elderlys: elderlys,
      }
    )

    res.json(elderly)
  }
})

//delete a device
router.delete('/delete', async (req, res) => {
  const elderlyId = req.body.elderlyId
  await Elderly.deleteOne({ _id: elderlyId })

  const userId = req.body.userId
  const dbUser = await User.findById(userId)

  if (dbUser) {
    const elderlys = dbUser.elderlys
    elderlys.pop(elderlyId)

    await User.updateOne(
      { _id: userId },
      {
        elderlys: elderlys,
      }
    )
  }

  res.status(204).json()
})

//get elderly information
router.get('/', async (req, res) => {
  const elderly = await Elderly.findById(req.query.id)

  if (elderly) {
    res.send({
      name: elderly.name,
      avatar: elderly.avatar,
      gender: elderly.gender,
      birthday: elderly.birthday,
      height: elderly.height,
      weight: elderly.weight,
      imei: elderly.imei,
      data: elderly.data,
      safeZones: elderly.safeZones,
      notifications: elderly.notifications,
    })
  } else {
    res.statusCode(404)
  }
})

//get elderly information
router.get('/imei', async (req, res) => {
  const elderly = await Elderly.findOne({ imei: req.query.imei })

  if (elderly) {
    res.send({
      name: elderly.name,
      avatar: elderly.avatar,
      gender: elderly.gender,
      birthday: elderly.birthday,
      height: elderly.height,
      weight: elderly.weight,
      imei: elderly.imei,
      data: elderly.data,
      safeZones: elderly.safeZones,
      notifications: elderly.notifications,
      id: elderly._id,
    })
  } else {
    res.statusCode(404)
  }
})

//update elderly information
router.post('/update', async (req, res) => {
  const elderly = await Elderly.findById(req.query.id)

  if (elderly) {
    elderly.name = req.body.name
    elderly.avatar = req.body.avatar
    elderly.gender = req.body.gender
    elderly.birthday = req.body.birthday
    elderly.height = req.body.height
    elderly.weight = req.body.weight
    await elderly.save()
    res.status(204).json()
  } else {
    res.status(404).json()
  }
})

//get all elderlies' information of user
router.get('/all', async (req, res) => {
  // todo this GPS information should be in the location endpoint
  const user = await User.findById(req.query.id).populate('elderlys')
  let list = []
  for (let elderly of user.elderlys) {
    let location = await Location.find({
      imei: elderly.imei,
      longitude: { $ne: null },
      latitude: { $ne: null },
    }).sort({ date: -1 })
    let longitude, latitude
    if (location.length == 0) {
      longitude = 174.770119
      latitude = -36.852745
    } else {
      longitude = location[0].longitude
      latitude = location[0].latitude
    }
    list.push({
      avatar: elderly.avatar,
      name: elderly.name,
      _id: elderly._id,
      imei: elderly.imei,
      longitude,
      latitude,
    })
  }
  res.json(list)
})

//delete elderly
router.delete('/remove', async (req, res) => {
  let user = await User.findById(req.body.userId)
  const index = user.elderlys.indexOf(req.body.elderlyId)
  if (index > -1) {
    user.elderlys.splice(index, 1)
  }
  await user.save()
  await Elderly.deleteOne({ _id: req.body.elderlyId })
  res.status(204).json()
})

export default router
