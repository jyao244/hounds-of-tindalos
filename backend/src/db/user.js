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
  avatar: String,
  elderlys: [{ type: String, ref: 'Elderly', default: [] }],
})

const User = mongoose.model('User', userSchema)

export default User;