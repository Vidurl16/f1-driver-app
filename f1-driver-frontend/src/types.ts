export interface DriverDto {
  driverId: number;
  name: string;
  team: string;
  country: string;
  championshipPoints: number;
}

export interface CreateDriverDto {
  name: string;
  team: string;
  country: string;
  championshipPoints: number;
}
