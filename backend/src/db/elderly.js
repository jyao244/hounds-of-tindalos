import mongoose from 'mongoose'
const Schema = mongoose.Schema

const elderlySchema = new Schema({
    name: String,
    avatar: String,
    imei: {
      type: String,
      required: true,
    },
    gender: String,
    birthday: Date,
    height: Number,
    weight: Number,
    wear: Boolean,
    saveZone: Boolean,
    powerWarning: Boolean,
})
  
const Elderly = mongoose.model('Elderly', elderlySchema)

export default Elderly;