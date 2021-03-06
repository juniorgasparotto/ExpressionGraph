### Encontrando todos os ascendentes de uma entidade <header-set anchor-name="search-find-ascending" />

Para encontrar os ascendentes de uma entidade, devemos verificar se a entidade anterior tem seu **nível geral** menor que o **nível geral** da entidade desejada. Se tiver, essa entidade é uma ascendente.

```
                A + B
Level:          1   2
                ^   *
Parent of B:    A
```

Se a entidade anterior for do mesmo nível da entidade deseja, deve-se ignora-la e continuar navegando para trás até encontrar a primeira entidade com o **nível geral** menor que o **nível geral** da entidade desejada. 

```
                A + B + J
Level:          1   2   2
                ^       *
Parent of J:    A
```

Após encontrar o primeiro ancestral, deve-se continuar navegando para trás, porém o **nível geral** a ser considerado agora será o do primeiro ancestral e não mais da entidade desejada. Esse processo deve continuar até chegar na entidade raiz.

```
                A + B + (J + Y)
Level:          1   2    2   3
                ^        ^   *
Parents of Y:   J, A
```

**Atenção:** Essa pesquisa tem diferenças nos tipos: **Pesquisa profunda** e **Pesquisa superficial**. Contudo, a _pesquisa profunda_ pode retornar uma quantidade maior de ocorrências. Isso ocorre por que nesse tipo de pesquisa os grupos de expressões são declarados todas as vezes que a entidade pai for utilizada.

Por exemplo, se quisermos pegar os ascendentes da entidade "C" considerando todas as suas ocorrências:

**Primeira ocorrência:**

* A entidade "C" da linha "#02" tem o nível geral igual a "2".
* `#01`: A entidade "B" tem o nível geral igual a "2". Não é um ascendente.
* `#00`: **A entidade "A" tem o nível geral igual a "1" (é menor), portanto, é a primeira ascendente (entidade pai). Agora o nível a ser considerado será o nível "1" e não mais o nível "2"**.

A expressão chegou a fim e teremos as seguintes entidades ascendentes: `A`

**Segunda ocorrência:**

* A entidade "C" da linha "#09" tem o nível geral igual a "5".
* `#08`: A entidade "B" tem o nível geral igual a "5", não é uma ascendente.
* `#07`: **A entidade "G" tem o nível geral igual a "4" (é menor), portanto, é a primeira ascendente (entidade pai). Agora o nível a ser considerado será o nível "4" e não mais o nível "5"**.
* `#06`: **A entidade "F" tem o nível geral igual a "3". Ela tem o nível geral menor que a entidade "G", portanto, é uma ascendente. Agora o nível a ser considerado será o nível "3" e não mais o nível "4"**.
* `#05`: A entidade "E" tem o nível geral igual a "3". Não é uma ascendente.
* `#04`: **A entidade "D" tem o nível geral igual a "2". Ela é uma ascendente. Agora o nível a ser considerado será o nível "2" e não mais o nível "3"**.
* `#03`: A entidade "Y" tem o nível geral igual a "3". Não é uma ascendente.
* `#02`: A entidade "C" tem o nível geral igual a "2". Não é uma ascendente.
* `#01`: A entidade "B" tem o nível geral igual a "2". Não é uma ascendente.
* `#00`: **A entidade "A" tem o nível geral igual a "1". Ela é uma ascendente. Agora o nível a ser considerado será o nível "1" e não mais o nível "2"**.

A expressão chegou ao fim e teremos as seguintes entidades ascendentes: `G, F, D, A`