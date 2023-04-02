import { Component } from '@angular/core';
import { TasksService } from '../api/tasks.service';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { ProjectDataDialogComponent } from '../project-data-dialog/project-data-dialog.component';

@Component({
  selector: 'app-project-data',
  templateUrl: './project-data.component.html',
  styleUrls: ['./project-data.component.css']
})
export class ProjectDataComponent {
  items_task: any
  items_mandatory: Array<any> = []
  items_optional: Array<any> = []
  length = 50;
  pageSize = 5;
  pageIndex = 0;
  pageSizeOptions = [5, 10, 25];

  hidePageSize = false;
  showPageSizeOptions = true;
  showFirstLastButtons = true;
  disabled = false;

  constructor(private taskService: TasksService, public dialog: MatDialog) {
    this.get_pagina_task()
    this.get_pagina_courses()
  }

  handlePageEvent(e: PageEvent) {
    this.length = e.length;
    this.pageSize = e.pageSize;
    this.pageIndex = e.pageIndex;
    this.get_pagina_task()
  }

  setPageSizeOptions(setPageSizeOptionsInput: string) {
    if (setPageSizeOptionsInput) {
      this.pageSizeOptions = setPageSizeOptionsInput.split(',').map(str => +str);
    }
  }

  get_pagina_task() {
    this.taskService.get_pagina_task(this.pageIndex * this.pageSize, this.pageSize).subscribe({
      next: (response) => {
        console.log(response)
        this.items_task = response.data;
        this.length = response.recordTotal;
      },
      error: (error) => {
      }
    })
  }

  get_pagina_courses() {
    this.taskService.get_pagina_courses(this.pageIndex * this.pageSize, this.pageSize).subscribe({
      next: (response) => {
        console.log(response)
        response.data.forEach((element: any) => {
          if (element.isMandatory)
            this.items_mandatory.push(element);
          else
            this.items_optional.push(element);
        });
        this.length = response.recordTotal;
      },
      error: (error) => {
      }
    })
  }

  openDialog(item: any) {
    this.dialog.open(ProjectDataDialogComponent, {
      width: "30%",
      height: "60%",
      data: {
        item
      },
    }).afterClosed().subscribe(val => { });
  }
}

