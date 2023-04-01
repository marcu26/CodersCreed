import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { ProfileComponent } from './auth/profile/profile.component';
import { ResetPasswordComponent } from './auth/reset-password/reset-password.component';
import { ProjectsComponent } from './projects/projects.component';

const routes: Routes = [
	{ path: "projects", component: ProjectsComponent },
	{ path: "login", component: LoginComponent },
	{ path: "profile", component: ProfileComponent },
	{ path: "reset-password", component: ResetPasswordComponent },
	{ path: "**", component: LoginComponent },
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule { }
