import mongoose from 'mongoose'
const Schema = mongoose.Schema

const temperatureSchema = new Schema({
    imei: {
      type: String,
      required: true,
    },
    date: {
      type: Date,
      required: true,
    },
    temp: Number,
})
  
const Temperature = mongoose.model('Temperature', temperatureSchema)

export default Temperature