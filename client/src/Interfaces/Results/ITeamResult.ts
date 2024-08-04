interface ITeam{
  id: string
  name: string
}

interface ITeamResult {
  value:{
    items: ITeam[]
  }
  isSuccess: boolean
  statusCode: number
  message: string
}
