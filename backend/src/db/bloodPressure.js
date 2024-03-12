import mongoose from 'mongoose'
const Schema = mongoose.Schema

const bloodPressureSchema = new Schema({
    imei: {
      type: String,
      required: true,
    },
    date: {
      type: Date,
      required: true,
    },
    highPressure: Number,
    lowPressure: Number,
})
  
const BloodPressure = mongoose.model('BloodPressure', bloodPressureSchema)

export default BloodPressure