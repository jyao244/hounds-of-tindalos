import mongoose from 'mongoose'
const Schema = mongoose.Schema

const accuracySchema = new Schema({
    accuracy: Number,
    battery: Number,
})
  
const Accuracy = mongoose.model('Accuracy', accuracySchema)

export default Accuracy;