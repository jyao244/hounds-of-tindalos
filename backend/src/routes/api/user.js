import express from 'express'
import { verify } from './verifyToken'
import User from '../../db/user'
import bcrypt from 'bcrypt'
import jwt from 'jsonwebtoken'

const router = express.Router()

// Get all user information
router.get('/', async (req, res) => {
  const user = await User.findById(req.query.id)

  if (user) {
    res.send({
      username: user.username,
      email: user.email,
      avatar: user.avatar,
      elderlys: user.elderlys,
    })
  } else {
    res.statusCode(404)
  }
})

//update user information
router.post('/update', async (req, res) => {
  const user = await User.findById(req.query.id)

  if (user) {
    if (req.body.username) {
      user.username = req.body.username
    }
    if (req.body.email) {
      user.email = req.body.email
    }
    if (req.body.avatar) {
      user.avatar = req.body.avatar
    }
    await user.save()
    res.status(204).json()
  } else {
    res.status(404).json()
  }
})

//update password
router.post('/password', async (req, res) => {
  const user = await User.findById(req.query.id)

  //Hash password
  const salt = await bcrypt.genSalt(10)
  const hashPassword = req.body.password
    ? await bcrypt.hash(req.body.password, salt)
    : null

  if (user) {
    if (hashPassword) {
      user.password = hashPassword
    }
    await user.save()
    res.status(204).json()
  } else {
    res.status(404).json()
  }
})

//get all users
router.get('/all', async (req, res) => {
  const users = await User.find()
  res.send(users)
})

//delete user
router.delete('/delete', async (req, res) => {
  const user = await User.findById(req.query.id)

  if (user) {
    await user.remove()
    res.status(204).json()
  } else {
    res.status(404).json()
  }
})

export default router
