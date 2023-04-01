import { ProjectsComponent } from './projects/projects.component';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { CoursePreviewComponent } from './course-preview/course-preview.component';

const routes: Routes = [
	{ path: "projects", component: ProjectsComponent },
	{ path: "login", component: LoginComponent },
	{ path: "profile", component: CoursePreviewComponent },
	{ path: "**", component: LoginComponent },
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule { }
