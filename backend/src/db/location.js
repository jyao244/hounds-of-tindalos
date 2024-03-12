import mongoose from 'mongoose'
const Schema = mongoose.Schema

const locationSchema = new Schema({
    imei: {
      type: String,
      required: true,
    },
    date: {
      type: Date,
      required: true,
    },
    wear: Boolean,
    longitude: Number,
    latitude: Number,
    battery: Number,
})

const Location = mongoose.model('Location', locationSchema)

export default Location