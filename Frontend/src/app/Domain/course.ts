export class Course {
  id: number;
  name: string;
  seasonType: string;
  numberOfDays: number;
  description: string;
  price: number;

  constructor(
    id: number,
    name: string,
    seasonType: string,
    numberOfDays: number,
    description: string,
    price: number
  ) {
    this.id = id;
    this.name = name;
    this.seasonType = seasonType;
    this.numberOfDays = numberOfDays;
    this.description = description;
    this.price = price;
  }
}
