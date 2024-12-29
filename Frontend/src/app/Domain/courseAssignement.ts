export class CourseAssigment {
  id!: number;
  courseId!: number;
  instructorId!: number;
  startDate!: Date;
  endDate!: Date;

  constructor() {}

  initialize(
    courseAssigmentId: number,
    courseId: number,
    instructorId: number,
    startDate: Date,
    endDate: Date
  ): void {
    this.id = courseAssigmentId;
    this.courseId = courseId;
    this.instructorId = instructorId;
    this.startDate = startDate;
    this.endDate = endDate;
  }
}

export class CreateCourseAssigment {
  courseId!: number;
  instructorId!: number;
  startDate!: Date;
  endDate!: Date;

  constructor() {}

  initialize(
    courseId: number,
    instructorId: number,
    startDate: Date,
    endDate: Date
  ): void {
    this.courseId = courseId;
    this.instructorId = instructorId;
    this.startDate = startDate;
    this.endDate = endDate;
  }
}
