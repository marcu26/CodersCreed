import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-reward-dialog',
  templateUrl: './reward-dialog.component.html',
  styleUrls: ['./reward-dialog.component.css']
})
export class RewardDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<RewardDialogComponent>,
  ) { }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
