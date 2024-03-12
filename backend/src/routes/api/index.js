import express from 'express'
import auth from './auth'
import user from './user'
import elderly from './elderly'
import data from './data'
import safeZone from './safeZone'
import notification from './notification'

const router = express.Router()

router.use('/auth', auth)
router.use('/user', user)
router.use('/elderly', elderly)
router.use('/data', data)
router.use('/safezone', safeZone)
router.use('/notification', notification)

export default router
