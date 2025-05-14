src/
├── core/ <-- 💡 Game brain (no dependencies)
│ ├── entities/ <-- Pet, Stat, Item, Event
│ ├── usecases/ <-- FeedPet, PlayWithPet, TickGameClock
│ ├── rules/ <-- Aging, stat decay, leveling
│ └── interfaces.ts <-- IPetRepo, IStorageService, etc.
│
├── services/ <-- 🔌 Implementations of interfaces
│ ├── petRepo.ts <-- Uses localStorage, IndexedDB, etc.
│ ├── storageService.ts <-- For persistent saves
│ └── timerService.ts <-- For game loop or tick logic
│
├── infrastructure/ <-- 🌐 Third-party adapters or libs
│ ├── localStorageAdapter.ts
│ └── animationLibrary.ts <-- e.g. GSAP or CSS transitions
│
├── ui/ <-- 🎨 UI layer
│ ├── components/ <-- PetView, StatBar, Inventory
│ ├── screens/ <-- GameScreen, StartMenu, Settings
│ └── App.tsx <-- Composes everything
│
├── hooks/ <-- 🪝 Glue UI to logic
│ ├── usePet.ts
│ └── useGameLoop.ts
│
└── index.tsx <-- Entry point
