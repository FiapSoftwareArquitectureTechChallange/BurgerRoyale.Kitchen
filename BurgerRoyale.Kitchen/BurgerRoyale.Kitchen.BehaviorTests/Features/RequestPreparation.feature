Feature: RequestPreparation

Scenario: Request Order Preparation
	Given An order arrives in the system
	When User places an order
	Then Create order in the database
