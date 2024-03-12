import mongoose from 'mongoose'
const Schema = mongoose.Schema

const bloodOxygenSchema = new Schema({
    imei: {
      type: String,
      required: true,
    },
    date: {
      type: Date,
      required: true,
    },
    bloodOxygen: Number,
})
  
const BloodOxygen = mongoose.model('BloodOxygen', bloodOxygenSchema)

export default BloodOxygen