export class AppUser {
  id: number = 0;
  firstName: string = '';
  lastName: string = '';
  phoneNumber: string = '';
  email: string = '';
  amountPaid: number = 0;
  amountToBePaid: number = 0;
}

export class CreateAppUserDto {
  firstName: string = '';
  lastName: string = '';
  phoneNumber: string = '';
  email: string = '';
}
