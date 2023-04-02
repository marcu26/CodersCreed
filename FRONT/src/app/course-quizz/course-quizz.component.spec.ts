import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseQuizzComponent } from './course-quizz.component';

describe('CourseQuizzComponent', () => {
  let component: CourseQuizzComponent;
  let fixture: ComponentFixture<CourseQuizzComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CourseQuizzComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CourseQuizzComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
