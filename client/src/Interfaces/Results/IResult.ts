interface IResult<T = any> {
  value: T
  isSuccess: boolean
  statusCode: number
  message: string
}
