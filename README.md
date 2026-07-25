# OrbitWatch

A web application that visualizes Near-Earth Objects (NEOs) using NASA's public asteroid data. The project combines a 3D interactive Earth visualization with asteroid trajectory data and basic threat analysis to create an educational and visually engaging experience.

> **Project Status:** 🚧 Early Development

## Features (Planned)

* Fetch Near-Earth Object data from NASA's NeoWs API
* Interactive 3D Earth visualization
* Display asteroid trajectories around Earth
* Basic threat assessment based on object size, velocity, and miss distance
* Dashboard showing asteroid details and statistics
* Search and filter NEOs by date or threat level

## Tech Stack

### Backend

* ASP.NET Core Web API
* C#
* NASA NeoWs API
* In-memory caching (Redis may be added later)

### Frontend

* React (Vite)
* React Three Fiber (Three.js)
* Tailwind CSS
* TanStack Query

## Project Structure

```text
neo-trajectory-visualizer/
├── backend/      # ASP.NET Core Web API
├── frontend/     # React application
└── README.md
```

## Roadmap

* [ ] Set up backend project
* [ ] Connect to NASA NeoWs API
* [ ] Create backend endpoints
* [ ] Build React frontend
* [ ] Render Earth in 3D
* [ ] Display asteroid trajectories
* [ ] Implement threat scoring
* [ ] Improve visuals and UI
* [ ] Deploy application

## Inspiration

This project aims to combine space data, physics, and interactive 3D graphics into a portfolio project that demonstrates both backend engineering and modern frontend visualization.

## License

This project is licensed under the MIT License.
