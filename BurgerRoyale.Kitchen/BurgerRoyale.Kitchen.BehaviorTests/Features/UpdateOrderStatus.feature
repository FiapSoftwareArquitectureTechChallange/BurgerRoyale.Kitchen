Feature: UpdateOrderStatus

Scenario: Update Order Status
	Given An order exists in the database
	When The Kitchen fetches orders
	Then Update order status
