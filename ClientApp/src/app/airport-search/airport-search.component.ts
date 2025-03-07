import { Component, OnInit } from '@angular/core';
import { AirportService } from '../airport.service';
import { SearchHistoryService } from '../search-history.service';
import { NgIf, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-airport-search',
  standalone: true,
  imports: [NgIf, NgFor, FormsModule],
  providers: [AirportService, SearchHistoryService],
  templateUrl: './airport-search.component.html',
  styleUrls: ['./airport-search.component.scss']
})
export class AirportSearchComponent implements OnInit {
  airports: any[] = [];
  searchTerm: string = '';
  filteredSuggestions: string[] = [];
  searchHistory: string[] = [];

  cities: string[] = [];
  countries: string[] = [];
  iatas: string[] = [];

  selectedCity: string = '';
  selectedCountry: string = '';
  selectedIata: string = '';
  selectedSort: string = '';

  constructor(
    private airportService: AirportService,
    private searchHistoryService: SearchHistoryService
  ) {}

  ngOnInit(): void {
    this.loadSearchHistory();
  }

  // Load search history on component initialization
  loadSearchHistory(): void {
    this.searchHistoryService.getSearchHistory().subscribe(
      (history) => {
        this.searchHistory = history;
      },
      (error) => {
        console.error('Error fetching search history:', error);
      }
    );
  }

  // Handle search input and filter suggestions from search history + airports
  onSearchInput(): void {
    if (!this.searchTerm.trim()) {
      this.filteredSuggestions = [];
      return;
    }

    const lowerSearch = this.searchTerm.toLowerCase();
    const historyMatches = this.searchHistory.filter(term => term.toLowerCase().includes(lowerSearch));
    const airportMatches = this.airports
      .map(a => a.name)
      .filter(name => name.toLowerCase().includes(lowerSearch));

    this.filteredSuggestions = [...new Set([...historyMatches, ...airportMatches])];
  }

  // Perform search and update results
  onSearch(): void {
    if (!this.searchTerm.trim()) return;

    this.airportService.searchAirports(this.searchTerm).subscribe(
      (data) => {
        this.airports = data;
        this.cities = [...new Set(this.airports.map(a => a.city))];
        this.countries = [...new Set(this.airports.map(a => a.country))];
        this.iatas = [...new Set(this.airports.map(a => a.iata))];

        this.updateSearchHistory(this.searchTerm);
      },
      (error) => {
        console.error('Error fetching airports:', error);
      }
    );
  }

  // Update search history (limit to 10 entries)
  updateSearchHistory(term: string): void {
    if (!this.searchHistory.includes(term)) {
      this.searchHistory.unshift(term);
      if (this.searchHistory.length > 10) this.searchHistory.pop();
    }
  }

  // Select a suggestion from the dropdown
  selectSuggestion(suggestion: string): void {
    this.searchTerm = suggestion;
    this.filteredSuggestions = [];
    this.onSearch();
  }

  // Sort airports based on selected sort criteria
  handleSortSelection(): void {
    if (this.selectedSort) {
      this.airports = [...this.airports].sort((a, b) => 
        typeof a[this.selectedSort] === 'string' 
          ? a[this.selectedSort].localeCompare(b[this.selectedSort]) 
          : a[this.selectedSort] - b[this.selectedSort]
      );
    }
  }

  // Filter airports by city
  handleCitySelection(): void {
    if (this.selectedCity) {
      this.airports = this.airports.filter(a => a.city === this.selectedCity);
    }
  }

  // Filter airports by country
  handleCountrySelection(): void {
    if (this.selectedCountry) {
      this.airports = this.airports.filter(a => a.country === this.selectedCountry);
    }
  }

  // Filter airports by IATA code
  handleIATASelection(): void {
    if (this.selectedIata) {
      this.airports = this.airports.filter(a => a.iata === this.selectedIata);
    }
  }
}
