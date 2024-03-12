import express from 'express'
import Notification from '../../db/notification'
import User from '../../db/user'
import Elderly from '../../db/elderly'
import Location from '../../db/location'

const router = express.Router()

//get all notification info of user
router.get('/all', async (req, res) => {
  const user = await User.findById(req.query.id).populate('elderlys')
  var list = []
  for (var elderly of user.elderlys) {
    var notification = await Notification.find({ imei: elderly.imei })
    if(notification.length > 0){
      var tem = [];
      for(var i = 0; i < notification.length; i++){
        tem.push({...notification[i]._doc, avatar: elderly.avatar})
      }
      notification = tem;
    }
    list = list.concat(notification);
  }
  console.log(list)
  list = list.sort((a, b) => b.date - a.date)
  res.json(list)
})

//add notification info
router.post('/add', async (req, res) => {
  const elderly = await Elderly.findOne({imei: req.body.imei})

  const notification = new Notification({
    date: req.body.date,
    imei: req.body.imei,
    title: req.body.title,
    subtitle: "The wearer " + elderly.name + req.body.subtitle,
  })

  const location = new Location({
    imei: req.body.imei,
    date: req.body.date,
    longitude: req.body.longitude,
    latitude: req.body.latitude,
    wear: true,
  })

  try {
    await notification.save()
    await location.save()
    res.status(204).json()
  } catch {
    ;(err) => res.status(404).json()
  }
})

export default router
