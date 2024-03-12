import mongoose from 'mongoose'
const Schema = mongoose.Schema

const heartRateSchema = new Schema({
    imei: {
      type: String,
      required: true,
    },
    date: {
      type: Date,
      required: true,
    },
    steps: Number,
    heartRate: Number,
    deepSleep: Number,
    lightSleep: Number,
    startSleep: Date,
    endSleep: Date,
})
  
const HeartRate = mongoose.model('HeartRate', heartRateSchema)

export default HeartRate