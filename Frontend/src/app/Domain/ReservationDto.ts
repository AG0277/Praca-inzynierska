export class RoomReservationDto {
  roomId: number = 0;
  roomNumber: number = 0;
  noOfPeople: number = 0;
  from: Date = new Date();
  to: Date = new Date();
  totalPrice: number = 0;
  availableSpace: number = 0;
}

export class CreateRoomReservationDto {
  appUserId: number = 0;
  roomId: number = 0;
  numberOfBeds: number = 0;
  from: Date = new Date();
  to: Date = new Date();
  totalPrice: number = 0;
}

export function mapToCreateRoomReservationDto(
  dto: RoomReservationDto,
  userId: number
): CreateRoomReservationDto {
  const result = new CreateRoomReservationDto();
  result.appUserId = userId;
  result.roomId = dto.roomId;
  result.numberOfBeds = dto.noOfPeople;
  result.from = dto.from;
  result.to = dto.to;
  result.totalPrice = dto.totalPrice;
  return result;
}
