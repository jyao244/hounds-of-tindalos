import express from 'express'
import safeZoneDetect from '../../utils/algorithm'
import Notification from '../../db/notification'
import SafeZone from '../../db/safeZone'
import BloodOxygen from '../../db/bloodOxygen'
import BloodPressure from '../../db/bloodPressure'
import HeartRate from '../../db/heartRate'
import Location from '../../db/location'
import Temperature from '../../db/temperature'
import Elderly from '../../db/elderly'
import Accuracy from '../../db/accuracy'

const router = express.Router()

//get data
router.get('/details', async (req, res) => {
  const location = await Location.find({imei: req.query.imei}).sort({ date: -1 })
  const bloodOxygen = await BloodOxygen.find({imei: req.query.imei}).sort({date: -1,})
  const bloodPressure = await BloodPressure.find({imei: req.query.imei}).sort({date: -1,})
  const heartRate = await HeartRate.find({imei: req.query.imei}).sort({ date: -1 })
  const temperature = await Temperature.find({imei: req.query.imei}).sort({date: -1,})
  var wear, steps, rate, highPressure, lowPressure, deepSleep, lightSleep, startSleep, endSleep, temp, oxygen, longitude, latitude
  if(location.length==0){
    longitude=174.770119
    latitude=-36.852745
    wear=false
  }else{
    longitude=location[0].longitude
    latitude=location[0].latitude
    wear=location[0].wear
  }
  if(bloodOxygen.length==0){
    oxygen=0
  }else{
    oxygen=bloodOxygen[0].bloodOxygen
  }
  if(bloodPressure.length==0){
    highPressure=0
    lowPressure=0
  }else{
    highPressure=bloodPressure[0].highPressure
    lowPressure=bloodPressure[0].lowPressure
  }
  if(heartRate.length==0){
    steps=0
    rate=0
    deepSleep=0
    lightSleep=0
    startSleep=new Date("2022-01-01T00:00:00.000+00:00")
    endSleep=new Date("2022-01-01T00:00:00.000+00:00")
  }else{
    steps=heartRate[0].steps
    rate=heartRate[0].heartRate
    deepSleep=heartRate[0].deepSleep
    lightSleep=heartRate[0].lightSleep
    startSleep=heartRate[0].startSleep
    endSleep=heartRate[0].endSleep
  }
  if(temperature.length==0){
    temp=0
  }else{
    temp=temperature[0].temp
  }
  try {
    res.json({
      wear: wear,
      steps: steps,
      heartRate: rate,
      highPressure: highPressure,
      lowPressure: lowPressure,
      deepSleep: deepSleep,
      lightSleep: lightSleep,
      startSleep: startSleep,
      endSleep: endSleep,
      temp: temp,
      bloodOxygen: oxygen,
      longitude: longitude,
      latitude: latitude,
    })
  } catch {
    ;(err) => res.status(404).json()
  }
})

//receive data from watch
router.post('/add/bloodoxygen', async (req, res) => {
  const bloodoxygen = new BloodOxygen({
    imei: req.body.imei,
    date: req.body.date,
    bloodOxygen: req.body.bloodOxygen,
  })

  try {
    await bloodoxygen.save()
    res.status(204).json()
  } catch {
    ;(err) => res.status(404).json()
  }
})

//receive data from watch
router.post('/add/bloodpressure', async (req, res) => {
  const bloodpressure = new BloodPressure({
    imei: req.body.imei,
    date: req.body.date,
    highPressure: req.body.highPressure,
    lowPressure: req.body.lowPressure,
  })

  try {
    await bloodpressure.save()
    res.status(204).json()
  } catch {
    ;(err) => res.status(404).json()
  }
})

//receive data from watch
router.post('/add/heartrate', async (req, res) => {
  const heartrate = new HeartRate({
    imei: req.body.imei,
    date: req.body.date,
    steps: req.body.steps,
    heartRate: req.body.heartRate,
    deepSleep: req.body.deepSleep,
    lightSleep: req.body.lightSleep,
    startSleep: req.body.startSleep,
    endSleep: req.body.endSleep,
  })

  try {
    await heartrate.save()
    res.status(204).json()
  } catch {
    ;(err) => res.status(404).json()
  }
})

//receive data from watch
router.post('/add/temperature', async (req, res) => {
  const temperature = new Temperature({
    imei: req.body.imei,
    date: req.body.date,
    temp: req.body.temp,
  })

  try {
    await temperature.save()
    res.status(204).json()
  } catch {
    ;(err) => res.status(404).json()
  }
})

router.post('/add/location', async (req, res) => {
  console.log(req.body)

  const elderly = await Elderly.findOne({imei: req.body.imei})

  const location = new Location({
    imei: req.body.imei,
    date: req.body.date,
    longitude: req.body.longitude,
    latitude: req.body.latitude,
    wear: req.body.wear,
    battery: req.body.battery,
  })

  try {
    await location.save()
    res.status(204).json()

    //battery checking
    if (req.body.battery < 10 && elderly.powerWarning==false) {
      const notification = new Notification({
        imei: req.body.imei,
        date: req.body.date,
        title: 'Power warning',
        subtitle:
          'The power level of wearer ' +
          elderly.name +
          '\'s device is below 10%',
      })
      await notification.save()

      //update powerWarning
      elderly.powerWarning=true
      await elderly.save()
    }else if(req.body.battery >= 10){
      elderly.powerWarning=false
      await elderly.save()
    }

    //wearing state checking
    if (!req.body.wear&&elderly.wear==true) {
      const notification = new Notification({
        date: req.body.date,
        imei: req.body.imei,
        title: 'Taken off device',
        subtitle:
          'The wearer ' +
          elderly.name +
          ' has taken off the device',
      })
      await notification.save()
    }

    if(req.body.wear&&elderly.wear==false){
      const notification = new Notification({
        date: req.body.date,
        imei: req.body.imei,
        title: 'Weared device',
        subtitle:
          'The wearer ' +
          elderly.name +
          ' has weared the device',
      })
      await notification.save()
    }

    //change elderly state
    elderly.wear = req.body.wear
    await elderly.save()

    // find out all the valid saftzone for this user
    const safeZone = await SafeZone.find({ imei: req.body.imei })

    // if not set the safe zone yet
    if (safeZone.length == 0) {
      console.log('no safe zone set yet')
      return
    }

    console.log('start checking')
    const result = safeZoneDetect(
      req.body.longitude,
      req.body.latitude,
      safeZone
    )
    console.log('finish safe zone checking: ', result)

    if (!result&&elderly.saveZone==true) {
      const notification = new Notification({
        date: req.body.date,
        imei: req.body.imei,
        title: 'Leaved safe zone',
        subtitle:
          'The wearer ' +
          elderly.name +
          ' has leaved the safe zone',
      })
      await notification.save()
    }

    if(result&&elderly.saveZone==false){
      const notification = new Notification({
        date: req.body.date,
        imei: req.body.imei,
        title: 'Backed to safe zone',
        subtitle:
          'The wearer ' +
          elderly.name +
          ' has backed to the safe zone',
      })
      await notification.save()
    }

    //change elderly state
    elderly.saveZone = result
    await elderly.save()
  } catch {
    ;(err) => res.status(404).json()
  }
})

// get the latest location of user
// todo add verify + support multiple users
router.get('/location', async (req, res) => {
  const location = await Location.find({ imei: req.query.imei }).sort({
    date: -1,
  })
  try {
    if (location.length > 0) {
      res.json(location[0])
    } else {
      // default Auckland city address
      res.json({
        longitude: 174.770119,
        latitude: -36.852745,
      })
    }
  } catch {
    ;(err) => res.status(404).json()
  }
})

//get footprint
router.get('/footprint', async (req, res) => {
  const startTime = new Date(req.query.startTime).toISOString()
  const endTime = new Date(req.query.endTime).toISOString()
  const location = await Location.find({
    imei: req.query.imei,
    date: { $gte: startTime, $lte: endTime },
  }).sort({date: 1,})
  try {
    res.json(location)
  } catch {
    ;(err) => res.status(404).json()
  }
})

//get physiological data
router.get('/biology', async (req, res) => {
  const startTime = new Date(req.query.startTime).toISOString()
  const endTime = new Date(req.query.endTime).toISOString()
  const bloodOxygen = await BloodOxygen.find({imei: req.query.imei, date: { $gte: startTime, $lte: endTime}}).sort({date: 1,})
  const bloodPressure = await BloodPressure.find({imei: req.query.imei, date: { $gte: startTime, $lte: endTime}}).sort({date: 1,})
  const heartRate = await HeartRate.find({imei: req.query.imei, date: { $gte: startTime, $lte: endTime}}).sort({ date: 1 })
  const temperature = await Temperature.find({imei: req.query.imei, date: { $gte: startTime, $lte: endTime}}).sort({date: 1,})
  try {
    res.json({
      heartRate: heartRate,
      temperature: temperature,
      bloodOxygen: bloodOxygen,
      bloodPressure: bloodPressure,
    })
  } catch {
    ;(err) => res.status(404).json()
  }
})

//test
router.post('/test', async (req, res) => {
  const accuracy = req.body.accuracy
  const battery = req.body.battery

  const test = new Accuracy({
    accuracy: accuracy,
    battery: battery,
  })

  try{
    await test.save()
    res.status(204).json()
  }catch{
    ;(err) => res.status(404).json()
  }

})

export default router
