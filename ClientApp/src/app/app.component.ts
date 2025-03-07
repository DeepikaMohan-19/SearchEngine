import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';  // Import RouterModule
import { AuthService } from './auth.service';  // Your AuthService

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,  // Mark as standalone
  imports: [RouterModule],  // Import RouterModule to use router-outlet
  providers: [AuthService]  // Add your providers here
})
export class AppComponent {
  title = 'Angular App';
}
