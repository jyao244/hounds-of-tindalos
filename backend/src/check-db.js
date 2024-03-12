import mongoose from 'mongoose'
import Greeting from './db/greeting'
import { DB_URL } from './utils/global'

main()

async function main() {
  await mongoose.connect(DB_URL, {
    useNewUrlParser: true,
  })
  console.log('Connected to database!')

  // add checking data
  const greeting = new Greeting({ message: 'Hello, world!' })
  await greeting.save()
  console.log('Data added!')

  // Disconnect when complete
  await mongoose.disconnect()
  console.log('Disconnected from database!')
}
