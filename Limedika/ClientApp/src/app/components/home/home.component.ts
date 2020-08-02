import { Component, OnInit } from '@angular/core';
import { LocationService } from 'src/app/services/location.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  clientLocations: Location[];
  errorMessage: string;
  fileName: string = null;

  constructor(
    private locationService: LocationService
  ) {  }

  ngOnInit(): void {
    this.getLocations();
  }

  private getLocations(): void {
    this.locationService.getLocations().subscribe(locations =>
      this.clientLocations = locations);
  }

  updatePostCodesClicked(): void {
    this.locationService.updatePostCodes().subscribe(r =>
      this.getLocations(),
      error => this.errorMessage = error.error
      );
  }

  alertCloseClicked(): void {
    this.errorMessage = null;
  }

  onFileChanged($event): void {
    const file = $event.target.files[0];
    this.fileName = file.name;

    const fileReader = new FileReader();
    fileReader.readAsText(file, "UTF-8");
    fileReader.onload = () => {
      const parsedLocations = JSON.parse(fileReader.result as string);
      this.locationService.importLocations(parsedLocations).subscribe(r =>
        {
          this.getLocations();
          this.fileName = null;
        },
        error => this.errorMessage = error.error);
    };
    fileReader.onerror = (error) => {
      this.errorMessage = 'Klaida keliant dokumentą.';
    };
    console.log(file);
  }
}
