import mongoose from 'mongoose'
const Schema = mongoose.Schema

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

export default Notification