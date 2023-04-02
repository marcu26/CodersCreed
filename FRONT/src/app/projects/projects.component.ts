import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ProjectsService } from '../api/projects.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent {
  items: any
  length = 50;
  pageSize = 5;
  pageIndex = 0;
  pageSizeOptions = [5, 10, 25];

  hidePageSize = false;
  showPageSizeOptions = true;
  showFirstLastButtons = true;
  disabled = false;

  constructor(private projectsService: ProjectsService) {
    this.get_pagina()
  }

  handlePageEvent(e: PageEvent) {
    this.length = e.length;
    this.pageSize = e.pageSize;
    this.pageIndex = e.pageIndex;
    this.get_pagina()
  }

  setPageSizeOptions(setPageSizeOptionsInput: string) {
    if (setPageSizeOptionsInput) {
      this.pageSizeOptions = setPageSizeOptionsInput.split(',').map(str => +str);
    }
  }

  get_pagina() {
    this.projectsService.get_pagina(this.pageIndex * this.pageSize, this.pageSize).subscribe({
      next: (response) => {
        this.items = response.data;
        this.length = response.recordTotal;
      },
      error: (error) => {
      }
    })
  }
}
