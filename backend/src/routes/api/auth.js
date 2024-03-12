import express from 'express'
import jwt from 'jsonwebtoken'
import User from '../../db/user'
import bcrypt from 'bcrypt'
import { TOKEN_SECRET } from '../../utils/global'

const router = express.Router()

//register a user
router.post('/register', async (req, res) => {
  //Check if the email is already in the database
  const emailExist = await User.findOne({ email: req.body.email })
  if (emailExist) return res.status(400).send('Email already exists')

  //Hash password
  const salt = await bcrypt.genSalt(10)
  const hashPassword = await bcrypt.hash(req.body.password, salt)

  //Create a new user
  const user = new User({
    username: req.body.username,
    email: req.body.email,
    password: hashPassword,
    avatar: req.body.avatar,
  })
  try {
    const savedUser = await user.save()
    const token = jwt.sign({ _id: savedUser._id }, TOKEN_SECRET)
    res.send({ token, id: savedUser._id })
  } catch {
    ;(err) => res.status(400).send(err)
  }
})

//login a user
router.post('/login', async (req, res) => {
  // Check if the user exists
  const user = await User.findOne({ email: req.body.email })
  if (!user) return res.status(404).send('Account does not exist.')

  // Check Passwords
  const validPass = await bcrypt.compare(req.body.password, user.password)
  if (!validPass) return res.status(404).send('Incorrect password')

  //Create and sign a token
  const token = jwt.sign({ _id: user._id }, TOKEN_SECRET)
  res.json({
    avatar: user.avatar,
    username: user.username,
    email: user.email,
    id: user._id,
    token,
    elderlys: user.elderlys.length,
  })
})

export default router
