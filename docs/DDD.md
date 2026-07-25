# Domain-Driven Design

The template uses DDD-inspired modeling where it adds clarity without adding ceremony.

## Aggregates

`Customer` and `Order` are aggregate roots. They own state changes and protect invariants.

## Value Objects

`Address` is modeled as a record because equality is based on values rather than identity.

## Invariants

Examples:

- An order cannot be submitted without items.
- Only draft orders can be changed.
- Paid orders cannot be cancelled in the current sample workflow.
- Payment amount must be positive.

## Domain Events

Domain events capture important business facts:

- `CustomerCreatedDomainEvent`
- `OrderCreatedDomainEvent`
- `OrderSubmittedDomainEvent`

The current template stores events in aggregate roots. Dispatching events to handlers is a future extension point.

## Modeling Guidance

Prefer behavior-rich entities over anemic property bags when rules matter. Keep infrastructure attributes and persistence logic outside the domain.
