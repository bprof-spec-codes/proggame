import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskUploadComponent } from './task-upload.component';

describe('TaskUploadComponent', () => {
  let component: TaskUploadComponent;
  let fixture: ComponentFixture<TaskUploadComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TaskUploadComponent]
    });
    fixture = TestBed.createComponent(TaskUploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
