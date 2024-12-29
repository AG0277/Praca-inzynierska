export class Room {
  id: number;
  numberOfBeds: number;
  roomNumber: number;
  pricePerBed: number;
  imageBase64: string;
  numberOfReservedBeds: number;

  constructor(
    id: number,
    numberOfBeds: number,
    roomNumber: number,
    pricePerBed: number,
    imageBase64: string,
    numberOfReservedBeds: number
  ) {
    this.numberOfReservedBeds = numberOfReservedBeds;
    this.id = id;
    this.numberOfBeds = numberOfBeds;
    this.roomNumber = roomNumber;
    this.pricePerBed = pricePerBed;
    this.imageBase64 = imageBase64;
  }
}
