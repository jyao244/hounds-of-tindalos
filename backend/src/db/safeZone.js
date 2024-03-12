import mongoose from 'mongoose'
const Schema = mongoose.Schema

const safeZoneSchema = new Schema({
    imei: {
      type: String,
      required: true,
    },
    locations: [{ longitude: Number, latitude: Number }],
    name: String,
})

const SafeZone = mongoose.model('SafeZone', safeZoneSchema)

export default SafeZone