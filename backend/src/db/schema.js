import mongoose from 'mongoose'
const Schema = mongoose.Schema

const userSchema = new Schema({
  username: {
    type: String,
    required: true,
  },
  email: {
    type: String,
    required: true,
    unique: true,
  },
  password: {
    type: String,
    required: true,
  },
  avatar: {
    data: Buffer, // base64 encoded image
    contentType: String,
  },
  elderlys: [{ type: String, ref: 'Elderly', default: [] }],
})

const User = mongoose.model('User', userSchema)

const elderlySchema = new Schema({
  name: String,
  avatar: {
    data: Buffer,
    contentType: String,
  },
  imei: {
    type: String,
    required: true,
  },
  gender: String,
  birthday: Date,
  height: Number,
  weight: Number,
})

const Elderly = mongoose.model('Elderly', elderlySchema)

const dataSchema = new Schema({
  imei: {
    type: String,
    required: true,
  },
  date: {
    type: Date,
    required: true,
  },
  wear: Boolean,
  steps: Number,
  heartRate: Number,
  highPressure: Number,
  lowPressure: Number,
  deepSleep: Number,
  lightSleep: Number,
  temp: Number,
  bloodOxygen: Number,
  longitude: Number,
  latitude: Number,
})

const Data = mongoose.model('Data', dataSchema)

const safeZoneSchema = new Schema({
  imei: {
    type: String,
    required: true,
  },
  locations: [{ longitude: Number, latitude: Number }],
})
const SafeZone = mongoose.model('SafeZone', safeZoneSchema)

const notificationSchema = new Schema({
  imei: {
    type: String,
    required: true,
  },
  title: {
    type: String,
    required: true,
  },
  subtitle: {
    type: String,
    required: true,
  },
  date: {
    type: Date,
    required: true,
  },
})
const Notification = mongoose.model('Notification', notificationSchema)

export { User, Elderly, Data, SafeZone, Notification }
