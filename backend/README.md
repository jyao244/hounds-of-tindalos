# backend

### register a user

```
POST /auth/register

Body

email: string

username: string

password: string

Responses

200

{

"id": "62f255823cd172350dcadd8a"

}
```

### login a user

```
POST /auth/login

Body

email: string

password: string

Responses

200

{

"username": "yzhe819",

"emaiil": "yzhe819@gmail.com",

"id": "62f255823cd172350dcadd8a",

"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfaWQiOiI2MmQ0ZDJjZTc5ODgyYTVmOGZiZjJhMmQiLCJpYXQiOjE2NTgxMTQ4OTh9.7TdA3u0VRg-2voLryoE3bvtm0itaf57jWzVTgZFckXg"

}
```

### get user information

```
GET /user/

Headers

auth-token: string

Parameters

id: string  (user's id)

Responses

200

{

"username": "yzhe819",

"emaiil": "yzhe819@gmail.com"

}
```

### update user information

```
POST /user/update

Headers

auth-token: string

Parameters

id: string  (user's id)

Body

username: string

email: string

avatar: string

Responses

204
```

### update user password

```
POST /user/password

Headers

auth-token: string

Parameters

id: string  (user's id)

Body

old: string

password: string

Responses

204


```

### add a device

```
POST /elderly/add

Headers

auth-token: string

Body

userId: string

imei: string

Responses

200

elderly (json)


```

### delete a device

```
DELETE /elderly/delete

Headers

auth-token: string

Body

userId: string

imei: string

Responses

204
```

### get elderly information

```
GET /elderly/

Headers

auth-token: string

Parameters

id: string

Responses

200

elderly (json)
```

### update elderly information

```
POST /elderly/update

Headers

auth-token: string

Parameters

id: string

Body

name: string

avatar: string

gender: string

birthday: date

height: number

weight: number

Responses

204
```

### receive elderly data from watch

```
POST /data/add

Body

imei: string

date: date

wear: boolean

steps: number

heartRate: number

highPressure: number

lowPressure: number

deepSleep: number

lightSleep: number

temp: number

bloodOxygen: number

longitude: number

latitude: number

Responses

204
```
