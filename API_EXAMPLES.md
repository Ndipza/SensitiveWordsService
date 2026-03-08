# API_EXAMPLES.md

## Sanitize Text

### Request

POST `/api/sanitizer`

```
{
  "text": "SELECT * FROM users"
}
```

### Response

```
{
  "sanitizedText": "****** * **** users"
}
```

---

## Create Sensitive Word

POST `/api/sensitive-words`

```
{
  "word": "DROP TABLE"
}
```

---

## Update Sensitive Word

PUT `/api/sensitive-words/{id}`

```
{
  "word": "DELETE FROM"
}
```

---

## Delete Sensitive Word

DELETE `/api/sensitive-words/{id}`

---

## Get All Sensitive Words

GET `/api/sensitive-words`