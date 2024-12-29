export class Room {
  id: number;
  numberOfBeds: number;
  roomNumber: number;
  pricePerBed: number;
  imageBase64: string;

  constructor(
    id: number,
    numberOfBeds: number,
    roomNumber: number,
    pricePerBed: number,
    imageBase64: string
  ) {
    this.id = id;
    this.numberOfBeds = numberOfBeds;
    this.roomNumber = roomNumber;
    this.pricePerBed = pricePerBed;
    this.imageBase64 = imageBase64;
  }
}

export class UpdateRoomDto {
  id: number;
  numberOfBeds: number;
  roomNumber: number;
  pricePerBed: number;
  imageBase64: string;

  constructor(
    id: number,
    numberOfBeds: number,
    roomNumber: number,
    pricePerBed: number,
    imageBase64: string
  ) {
    this.id = id;
    this.numberOfBeds = numberOfBeds;
    this.roomNumber = roomNumber;
    this.pricePerBed = pricePerBed;
    this.imageBase64 = imageBase64;
  }
}

export class CreateRoomDto {
  numberOfBeds: number;
  roomNumber: number;
  pricePerBed: number;
  imageBase64: string;

  constructor(
    numberOfBeds: number,
    roomNumber: number,
    pricePerBed: number,
    image: string
  ) {
    this.numberOfBeds = numberOfBeds;
    this.roomNumber = roomNumber;
    this.pricePerBed = pricePerBed;
    this.imageBase64 = image;
  }
}
