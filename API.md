**List of products**

**GET** `http://127.0.0.1:8080/list?fromid=1&toid=4`

**Success response:**

```json
{
	"success": true,
	"products":
		[
			{"id": 1, "name": "Mars", "quantity": 10, "price": 10.99},
			{"id": 2, "name": "Snickers", "quantity": 20, "price": 11.77},
			{"id": 3, "name": "Pepsi", "quantity": 30, "price": 8.9},
			{"id": 4, "name": "Fanta", "quantity": 40, "price": 7.74}
		]
}
```

**GET** `http://127.0.0.1:8080/list?fromid=10&toid=44`

**Success response:**

```json
{
	"success": true,
	"products":[]
}
```

**GET** `http://127.0.0.1:8080/list?fromid=1&toid=1`

**Success response:**

```json
{
	"success": true,
	"products":[{"id": 1, "name": "Mars", "quantity": 10, "price": 10.99}]
}
```

**Fail responses:**

```json
{
"success": false,
"error": "Bad GET request"
}
```

```json
{
"success": false,
"error": "Missing parameters for GET list request"
}
```

```json
{
"success": false,
"error": "Bad GET request"
}
```