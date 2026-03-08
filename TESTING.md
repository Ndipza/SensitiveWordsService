# TESTING.md

## Testing Strategy

The project includes multiple testing layers.

### Unit Tests

Focus on isolated components.

Examples:

* Trie insertion
* Sensitive word matching
* Sanitization logic

Frameworks used:

* xUnit
* FluentAssertions

---

### Integration Tests

Validate interactions between layers.

Examples:

* Controller endpoints
* Database interactions

---

### Edge Cases Tested

* Empty input
* Multiple sensitive words
* Overlapping phrases
* Case-insensitive matching

---