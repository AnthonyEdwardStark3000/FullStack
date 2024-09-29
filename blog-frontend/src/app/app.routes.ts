import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { BlogsComponent } from './components/blogs/blogs.component';
import { BlogComponent } from './components/blog/blog.component';
import { AboutComponent } from './components/about/about.component';

export const routes: Routes = [
    {
        path:"",component:HomeComponent
    },
    {
        path:"blogs",component:BlogsComponent
    },
    {
        path:"blog",component:BlogComponent
    },
    {
        path:"about",component:AboutComponent
    },
];
