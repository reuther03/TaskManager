interface IUser {
  value?: {
    fullName: string
    email: string
    profilePicture: string
  }
  isSuccess: boolean
  statusCode: number
  message: string
}
