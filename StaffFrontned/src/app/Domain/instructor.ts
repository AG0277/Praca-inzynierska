export class Instructor {
  id: number;
  firstName: string;
  lastName: string;
  phoneNumber: string;

  constructor(
    id: number,
    firstName: string,
    lastName: string,
    phoneNumber: string
  ) {
    this.id = id;
    this.firstName = firstName;
    this.lastName = lastName;
    this.phoneNumber = phoneNumber;
  }
}

export class CreateInstructorDto {
  firstName: string;
  lastName: string;
  phoneNumber: string;

  constructor(firstName: string, lastName: string, phoneNumber: string) {
    this.firstName = firstName;
    this.lastName = lastName;
    this.phoneNumber = phoneNumber;
  }
}

export class UpdateInstructorDto {
  id: number;
  firstName: string;
  lastName: string;
  phoneNumber: string;

  constructor(
    id: number,
    firstName: string,
    lastName: string,
    phoneNumber: string
  ) {
    this.id = id;
    this.firstName = firstName;
    this.lastName = lastName;
    this.phoneNumber = phoneNumber;
  }
}
