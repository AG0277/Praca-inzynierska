import { Room } from './room';

export class RoomReservation {
  reservationId: number = 0;
  appUserId: number = 0;
  roomId: number = 0;
  numberOfBeds: number = 0;
  from: Date = new Date();
  to: Date = new Date();
  amountPaid: number = 0;
  totalPrice: number = 0;
}

export class RoomAndRoomReservation {
  roomId: number = 0;
  roomNumber: number = 0;
  noOfPeople: number = 0;
  from: Date = new Date();
  to: Date = new Date();
  totalPrice: number = 0;
  numberOfBeds: number = 0;
  pricePerBed: number = 0;
  imageBase64: string = '';
}

export function mapToRoomAndRoomReservation(
  roomReservation: RoomReservation,
  room: Room
): RoomAndRoomReservation {
  const combined = new RoomAndRoomReservation();

  // Map RoomReservation fields
  combined.roomId = roomReservation.roomId;
  combined.roomNumber = room.roomNumber;
  combined.noOfPeople = roomReservation.numberOfBeds;
  combined.from = roomReservation.from;
  combined.to = roomReservation.to;
  combined.totalPrice = roomReservation.totalPrice;

  // Map Room fields
  combined.numberOfBeds = room.numberOfBeds;
  combined.pricePerBed = room.pricePerBed;
  combined.imageBase64 = room.imageBase64;

  return combined;
}
