function safeZoneCheck(longitude, latitude, locations) {
  let inside = false
  if (locations.length < 3) {
    return false
  }
  for (let i = 0, j = locations.length - 1; i < locations.length; i++) {
    const p1 = locations[i]
    const p2 = locations[j]
    if (
      (p1.latitude < latitude && p2.latitude >= latitude) ||
      (p2.latitude < latitude && p1.latitude >= latitude)
    ) {
      if (
        p1.longitude +
          ((latitude - p1.latitude) / (p2.latitude - p1.latitude)) *
            (p2.longitude - p1.longitude) <
        longitude
      ) {
        inside = !inside
      }
    }
    j = i
  }
  return inside
}

export default function safeZoneDetect(longitude, latitude, safeZones) {
  let result = false
  for (let i = 0; i < safeZones.length; i++) {
    result =
      result || safeZoneCheck(longitude, latitude, safeZones[i].locations)
  }
  return result
}
