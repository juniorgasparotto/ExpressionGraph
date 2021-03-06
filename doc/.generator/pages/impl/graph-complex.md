# Grafos complexos <header-set anchor-name="impl-graph-complex" />

Chamamos de grafos complexos aqueles que não contém tipo definido, ou seja, todos os itens são definidos como `object`. 

Esse tipo de grafo é presentado pela classe:

```csharp
GraphExpression.Expression<object> : List<EntityItem<object>>
```

Essa classe herda de `List<EntityItem<object>>`, ou seja, ela também é uma coleção da classe `EntityItem<object>`. A classe `EntityItem<object>` representa um item dentro da lista, é nessa classe que existem todas as informações da entidade no grafo.

No exemplo a seguir vamos converter um objeto do tipo `Class1` para o objeto `Expression<object>` e exibir todos os `EntityItem<object>` da estrutura do tipo `Class1`. Na última saída, vamos exibir como ficaria esse objeto no formato de expressão de grafo:

```csharp
public void GraphComplex()
{
    // create a simple object
    var model = new Class1
    {
        Class1_Prop1 = "Value1",
        Class1_Prop2 = new Class2()
        {
            Class2_Field1 = 1000,
            Class2_Prop2 = "Value2"
        }
    };

    // transversal navigation
    Expression<object> expression = model.AsExpression();
    foreach (EntityItem<object> item in expression)
    {
        var ident = new string(' ', item.Level * 2);
        var output = $"{ident}[{item.Index}] => Item: {GetEntity(item)}, Parent: {GetEntity(item.Parent)}, Previous: {GetEntity(item.Previous)}, Next: {GetEntity(item.Next)}, Level: {item.Level}";
        System.Console.WriteLine(output);
    }

    // Serialize to expression
    System.Console.WriteLine(expression.DefaultSerializer.Serialize());
}

// Get entity as String to example
private string GetEntity(EntityItem<object> item)
{
    if (item is PropertyEntity prop)
        return $"Property.{prop.Property.Name}";

    if (item is FieldEntity field)
        return $"Field.{field.Field.Name}";

    if (item is ComplexEntity root)
        return root.Entity.GetType().Name;

    return null;
}

public class Class1
{
    public string Class1_Prop1 { get; set; }
    public Class2 Class1_Prop2 { get; set; }
}

public class Class2
{
    public int Class2_Prop1 = int.MaxValue;
    public string Class2_Prop2 { get; set; }
}
```

**1)** Na primeira saída, podemos visualizar todas as informações do tipo `Class1` e também as informações da expressão de grafo: `Index`, `Parent`, `Next`, `Previous` e `Level`:

```
  [0] => Item: Class1, Parent: , Previous: , Next: Property.Class1_Prop1, Level: 1
    [1] => Item: Property.Class1_Prop1, Parent: Class1, Previous: Class1, Next: Property.Class1_Prop2, Level: 2
    [2] => Item: Property.Class1_Prop2, Parent: Class1, Previous: Property.Class1_Prop1, Next: Property.Class2_Prop2, Level: 2
      [3] => Item: Property.Class2_Prop2, Parent: Property.Class1_Prop2, Previous: Property.Class1_Prop2, Next: Field.Class2_Field1, Level: 3
      [4] => Item: Field.Class2_Field1, Parent: Property.Class1_Prop2, Previous: Property.Class2_Prop2, Next: , Level: 3
```

* A propriedade `Level` é a responsável por informar em qual nível do grafo está cada item, possibilitando criar uma saída identada que representa a hierarquia do objeto `model`.
* O método `GetEntity` é apenas um ajudante que imprime o tipo do item e o nome do membro que pode ser uma propriedade ou um campo. Poderíamos também retornar o valor do membro, mas para deixar mais limpo a saída, eliminamos essa informação.

**2)** Na segunda saída, veremos como ficou a expressão de grafo desse objeto:

<anchor-get name="impl-serialization-complex">Clique aqui</anchor-get> para entender como funciona a serialização de objetos complexos.

```
"Class1.32854180" + "Class1_Prop1: Value1" + ("Class1_Prop2.36849274" + "Class2_Prop2: Value2" + "Class2_Field1: 1000")
```

O método de extensão `AsExpression` é o responsável pela criação da expressão complexa. Esse método vai navegar por todos os nós partindo da raiz até o último descendente. Esse método contem os seguintes parâmetros:

* `ComplexExpressionFactory factory = null`: Esse parâmetro deve ser utilizado quando for necessário trocar ou estender o comportamento padrão de criação de uma expressão de grafo complexa. O tópico <anchor-get name="impl-factory-expression-complex" /> trás todas as informações de como estender o comportamento padrão.
* `bool deep = false`: Quando `true`, a expressão será profunda, ou seja, quando possível, vai repetir entidades que já foram navegadas. Veja o tópico <anchor-get name="search-deep" /> para entender o propósito dessa funcionalidade.

Esse método está disponível em todos os objetos .NET, basta apenas adicionar a referência do namespace: `using GraphExpression`.

**Conclusão:**

Nesse tópico vimos como é simples navegar em objetos complexos, abrindo caminhos para pesquisas e serializações.

Vejam também o tópico <anchor-get name="impl-factory-entity-complex" />, isso mostrará uma outra forma de criar objetos complexos.

## Elementos padrão de uma expressão de grafo para tipos complexos

Os elementos de uma expressão complexa (`Expression<object>`) podem variar entre os seguintes tipos:

* `ComplexEntity`: Esse tipo é a base de todos os outros tipos de uma expressão complexa. É também o tipo da entidade raiz, ou seja, da primeira entidade da expressão.
* `PropertyEntity`: Determina que o item é uma propriedade.
* `FieldEntity`: Determina que o item é um campo.
* `ArrayItemEntity`: Determina que o item é um item de um `array`, ou seja, a classe pai será do tipo `Array`.
* `CollectionItemEntity`: Determina que o item é um item de uma coleção, ou seja, a classe pai será do tipo `ICollection`.
* `DynamicItemEntity`: Determina que o item é uma propriedade dinâmica, ou seja, a classe pai será do tipo `dynamic`.

Todos esses tipos herdam de `ComplexEntity` que por sua vez herda de `EntityItem<object>`, portanto, além de suas propriedades especificas ainda terão as informações do item na expressão.

Ainda é possível estender a criação de uma expressões complexas. Para sabe mais veja o tópico <anchor-get name="impl-factory-expression-complex" />