## Tipos de pesquisas <header-set anchor-name="impl-search-kind" />

Por padrão, esse projeto trás os seguintes tipos de pesquisas:

* `Ancestors`: Retorna todos os antepassados de um determinado item.
* `AncestorsUntil`: Retorna todos os antepassados de um determinado item até que o filtro especificado retorne positivo.
* `Descendants`: Retorna todos os descendentes de um determinado item.
* `DescendantsUntil`: Retorna todos os descendentes de um determinado item até que o filtro especificado retorne positivo.
* `Children`: Retorna os filhos de um item.
* `Siblings`: Retorna os irmãos de um item.
* `SiblingsUntil`: Retorna os irmãos de um item até que o filtro especificado retorne positivo.

Todos esses tipos de pesquisas estão disponíveis para qualquer objeto dos tipos:

* `GraphExpression.EntityItem<T>`: Pesquisa com referência
* `IEnumerable<GraphExpression.EntityItem<T>>`: Pesquisa sem referência

Também é possível criar pesquisas customizadas usando os métodos de extensões do C#.

**Sem referências:**

```csharp
public static IEnumerable<EntityItem<T>> Custom<T>(this IEnumerable<EntityItem<T>> references)
```

**Com referências:**

```csharp
public static IEnumerable<EntityItem<T>> Custom<T>(this EntityItem<T> references)
```

### Delegates das pesquisa

Todos os métodos de pesquisa utilizam os seguintes delegates:

```csharp
public delegate bool EntityItemFilterDelegate<T>(EntityItem<T> item);
public delegate bool EntityItemFilterDelegate2<T>(EntityItem<T> item, int depth);
```

* `EntityItem<T> item`: Esse parâmetro significa o item corrente durante a pesquisa.
* `int depth`: Determina a profundidade do item corrente com relação a sua posição.

 Você pode usar as classes `Func<EntityItem<T>>` e `Func<EntityItem<T>, int>` para simplificar o uso.