import jwt from 'jsonwebtoken'
import { TOKEN_SECRET } from '../../utils/global'

export const verify = (req, res, next) => {
  const token = req.header('auth-token')
  if (!token) {
    return res.status(401).send('Access Denied')
  }
  try {
    const verified = jwt.verify(token, TOKEN_SECRET)
    req.user = verified
    next()
  } catch {
    ;(err) => res.status(400).send(err)
  }
}
