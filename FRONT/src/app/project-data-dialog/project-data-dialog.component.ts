import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { WebcamImage } from 'ngx-webcam';
import { Observable, Subject } from 'rxjs';
import { ProjectsService } from '../api/projects.service';

@Component({
  selector: 'app-project-data-dialog',
  templateUrl: './project-data-dialog.component.html',
  styleUrls: ['./project-data-dialog.component.css']
})
export class ProjectDataDialogComponent {


  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<ProjectDataDialogComponent>,
    public projectsService: ProjectsService
  ) { console.log(data) }

  onNoClick(): void {
    this.dialogRef.close();
  }

  openWeb = false;
  openSpinner = false;
  isTired = false;
  public webcamImage!: WebcamImage;
  private trigger: Subject<void> = new Subject<void>();
  triggerSnapshot(): void {
    console.log(this.openSpinner);

    this.openSpinner = true;
    this.openWeb = false;
    this.trigger.next();

    console.log(this.openSpinner);
  }

  handleImage(webcamImage: WebcamImage): void {
    console.info('Saved webcam image', webcamImage);

    this.projectsService.send_image(webcamImage).subscribe(data => {
      this.openSpinner = false;
      if (data.isObosit == true)
        alert("You look tired. Maybe you should take a break!")
    });
    this.webcamImage = webcamImage;
  }

  public get triggerObservable(): Observable<void> {
    return this.trigger.asObservable();
  }

  index = 0;
  isAptToWork() {
    this.index = (this.index + 1) % 2;
    if (this.index == 1)
      this.openWeb = true;
    else
      this.dialogRef.close();
  }

}
