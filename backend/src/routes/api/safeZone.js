import express from 'express'
import SafeZone from '../../db/safeZone'

const router = express.Router()

//add a safe zone
router.post('/add', async (req, res) => {
  const safeZone = new SafeZone({
    imei: req.body.imei,
    locations: req.body.locations,
    name: req.body.name,
  })

  try {
    await safeZone.save()
    res.status(204).json()
  } catch {
    ;(err) => res.status(404).json()
  }
})

//get safe zones
router.get('/', async (req, res) => {
  const data = await SafeZone.find({ imei: req.query.imei })
  res.json(data)
})

//delete safe zone
router.delete('/remove', async (req, res) => {
  await SafeZone.deleteOne({ _id: req.body.id })
  res.status(204).json()
})

export default router
