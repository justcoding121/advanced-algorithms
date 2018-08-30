### Note: 

Please don't take effort to create pull requests for new algorithms/data structures. This is just a curiosity driven personal hobby and [was originally not intented to be a library](https://github.com/justcoding121/Advanced-Algorithms/issues/2). Feel free fork and modify to fit your need if that's what you are looking for. You can however open issues/fix bugs with pull requests here, I would be happy to take a look when I get time.

# Advanced Algorithms

Various important computer science algorithms generically implemented in C#.

<a href="https://ci.appveyor.com/project/justcoding121/advanced-algorithms">![Build Status](https://ci.appveyor.com/api/projects/status/9xpcp4m87max2066?svg=true)</a>

Install by [nuget](https://www.nuget.org/packages/Advanced.Algorithms)

For beta releases on [beta branch](https://github.com/justcoding121/Advanced-Algorithms/tree/beta)

    Install-Package Advanced.Algorithms -Pre

Not a stable release yet.

* [API documentation](https://justcoding121.github.io/Advanced-Algorithms/api/Advanced.Algorithms.DataStructures.html)

Supports

 * .Net Standard 1.0 or above
 * .Net Framework 4.0 or above
 
## Data structures

### List

- [X] Array list (dynamic array) ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/List/ArrayList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Lists/ArrayList_Tests.cs))
- [X] Skip list ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/List/SkipList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Lists/SkipList_Tests.cs))

### HashSets

- [X] HashSet (using [separate chaining](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/HashSet/SeparateChainingHashSet.cs) optionally with [open address linear probing](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/HashSet/OpenAddressHashSet.cs)) ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/HashSet/HashSet.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/HashSet/HashSet_Tests.cs))
- [X] Sorted HashSet ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/HashSet/SortedHashSet.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/HashSet/SortedHashSet_Tests.cs))

### Dictionaries

- [X] Dictionary (using [separate chaining](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Dictionary/SeparateChainingDictionary.cs) optionally with [open address linear probing](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Dictionary/OpenAddressDictionary.cs)) ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Dictionary/Dictionary.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Dictionary/Dictionary_Tests.cs))
- [X] Sorted Dictionary ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Dictionary/SortedDictionary.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Dictionary/SortedDictionary_Tests.cs))

### Stack

- [X] Stack (using [dynamic array](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Stack/ArrayStack.cs) and optionally using [singly linked list](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Stack/LinkedListStack.cs)) ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Stack/Stack.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Stack_Tests.cs))

### Queue

- [X] Queue (using [dynamic array](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Queues/ArrayQueue.cs) and optionally using [doubly linked list](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Queues/LinkedListQueue.cs)) ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Queues/Queue.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Queues/Queue_Tests.cs))

#### Priority queue

- [X] Min priority queue ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Queues/PriorityQueue/MinPriorityQueue.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Queues/PriorityQueue/MinPriorityQueue_Tests.cs))
- [X] Max priority queue ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Queues/PriorityQueue/MaxPriorityQueue.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Queues/PriorityQueue/MaxPriorityQueue_Tests.cs))
 
### Linked list

- [X] Singly linked list ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/LinkedList/SinglyLinkedList.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/LinkedList/SinglyLinkedList_Tests.cs))
- [X] Doubly linked list ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/LinkedList/DoublyLinkedList.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/LinkedList/DoublyLinkedList_Tests.cs))
- [X] Circular linked list ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/LinkedList/CircularLinkedList.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/LinkedList/CircularLinkedList_Tests.cs))

### Heap

#### Min

- [X] Binary min heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Heap/Min/BMinHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Heap/Min/BMinHeap_Tests.cs))
- [X] d-ary min heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Heap/Min/d-aryMinHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Heap/Min/D-aryMinHeap_Tests.cs))
- [X] Binomial min heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Heap/Min/BinomialMinHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Heap/Min/BinomialMinHeap_Tests.cs))
- [X] Fibornacci min Hheap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Heap/Min/FibornacciMinHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Heap/Min/FibornacciMinHeap_Tests.cs))
- [X] Pairing min heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Heap/Min/PairingMinHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Heap/Min/PairingMinHeap_Tests.cs))

#### Max 

- [X] Binary max heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Heap/Max/BMaxHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Heap/Max/BMaxHeap_Tests.cs))
- [X] d-ary max heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Heap/Max/d-aryMaxHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Heap/Max/D-aryMaxHeap_Tests.cs))
- [X] Binomial max heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Heap/Max/BinomialMaxHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Heap/Max/BinomialMaxHeap_Tests.cs))
- [X] Fibornacci max heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Heap/Max/FibornacciMaxHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Heap/Max/FibornacciMaxHeap_Tests.cs))
- [X] Pairing max heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Heap/Max/PairingMaxHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Heap/Max/PairingMaxHeap_Tests.cs))

Note: It is observed that among the implementations here in practice, with the exclusion of DecrementKey/IncrementKey operation regular Binary Heap & d-ary Heap outperforms other in theory superiors. Likely because it don't have pointer juggling overhead and hey arrays are faster!

### Tree

- [X] Tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/Tree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/Tree_Tests.cs))
- [X] Binary tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/BinaryTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/BinaryTree_Tests.cs))

#### Binary search trees

- [X] Binary search tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/BST.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/BST_Tests.cs))
- [X] AVL tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/AvlTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/AVLTree_Tests.cs))
- [X] Red black tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/RedBlackTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/RedBlackTree_Tests.cs))
- [X] Splay tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/SplayTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/SplayTree_Tests.cs))
- [X] Treap tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/TreapTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/TreapTree_Tests.cs))

#### B trees (database trees)

- [X] B-tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/BTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/BTree_Tests.cs))
- [X] B+ tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/B%2BTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/B%2BTree_Tests.cs))

#### Queryable trees

- [X] Segment tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/SegmentTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/SegmentTree_Tests.cs))
- [X] Binary indexed tree (Fenwick tree) ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/FenwickTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/FenwickTree_Tests.cs))
- [X] Multi-dimensional interval tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/IntervalTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/IntervalTree_Tests.cs)) using nested red-black tree
- [X] Multi-dimensional k-d tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/K_DTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/KdTree_Tests.cs)) for range and nearest neigbour queries
- [X] Multi-dimensional range tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/RangeTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/RangeTreetests.cs)) using nested red-black tree
- [X] R-tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/RTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/RTree_Tests.cs))
- [X] Quadtree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/QuadTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/QuadTree_Tests.cs))

TODO: Support multi-dimentional segment tree & binary indexed tree.

#### Lookup trees

- [X] Prefix tree (Trie) ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/Trie.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/Trie_Tests.cs))
- [X] Suffix tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/SuffixTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/SuffixTree_Tests.cs))
- [X] Ternary search tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/TernarySearchTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Tree/TernarySearchTree_Tests.cs))

TODO: implement trie compression.

#### Set

- [X] Disjoint set ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Set/DisJointSet.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Set/DisJointSet_Tests.cs))
- [X] Sparse set ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Set/SparseSet.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Set/SparseSet_Tests.cs))
- [X] Bloom filter ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Set/BloomFilter.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Set/BloomFilter_Tests.cs))

### Graph

#### Adjacency list

- [X] Graph ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Graph/AdjacencyList/Graph.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyList/Graph_Tests.cs))
- [X] Weighted Graph ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Graph/AdjacencyList/WeightedGraph.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyList/WeightedGraph_Tests.cs))
- [X] DiGraph ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Graph/AdjacencyList/DiGraph.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyList/DiGraph_Tests.cs))
- [X] Weighted DiGraph ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Graph/AdjacencyList/WeightedDiGraph.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyList/WeightedDiGraph_Tests.cs))

#### Adjacency matrix

- [X] Graph ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Graph/AdjacencyMatrix/Graph.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyMatrix/Graph_Tests.cs))
- [X] Weighted Graph ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Graph/AdjacencyMatrix/WeightedGraph.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyMatrix/WeightedGraph_Tests.cs))
- [X] DiGraph ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Graph/AdjacencyMatrix/DiGraph.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyMatrix/DiGraph_Tests.cs))
- [X] Weighted DiGraph ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Graph/AdjacencyMatrix/WeightedDiGraph.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyMatrix/WeightedDiGraph_Tests.cs))


# Algorithms

## Graph algorithms

### Articulation points

- [X] Tarjan's articulation points finder ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/ArticulationPoint/TarjansArticulationFinder.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/ArticulationPoints/TarjansArticulation_Tests.cs))

### Bridges

- [X] Tarjan's bridge finder ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/Bridge/TarjansBridgeFinder.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/Bridge/TarjansBridge_Tests.cs))

### Connectivity

- [X] Kosaraju's strongly connected component finder ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/Connectivity/KosarajuStronglyConnected.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/Connectivity/KosarajuStronglyConnected_Tests.cs))
- [X] Tarjan's strongly connected component finder ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/Connectivity/TarjansStronglyConnected.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/Connectivity/TarjansStronglyConnected_Tests.cs))
- [X] Tarjan's bi-connected graph tester ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/Connectivity/TarjansBiConnected.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/Connectivity/TarjansBiConnected_Tests.cs))

### Coloring

- [X] M-coloring ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/Coloring/MColorer.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/Coloring/MColoring_Tests.cs))

### Cover

- [X] Min vertex cover ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/Cover/MinVertexCover.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/Cover/MinVertexCover.cs))

### Maximum flow

- [X] Ford-Fulkerson algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/Flow/FordFulkerson.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/Flow/FordFulkerson_Tests.cs))
- [X] Edmonds Karp's improvement ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/Flow/EdmondsKarp.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/Flow/EdmondsKarp_Tests.cs)) on Ford-Fulkerson algorithm
- [X] Push relabel algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/Flow/PushRelabel.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/Flow/PushRelabel_Tests.cs))

### Shortest path

- [X] Bellman-Ford algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/ShortestPath/Bellman-Ford.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/ShortestPath/BellmanFord_Tests.cs))
- [X] Dijikstra's algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/ShortestPath/Dijikstra.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/ShortestPath/Dijikstras_Tests.cs)) using Fibornacci heap.
- [X] Floyd-Warshall algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/ShortestPath/Floyd-Warshall.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/ShortestPath/FloydWarshall_Tests.cs))
- [X] Johnson's algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/ShortestPath/Johnsons.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/ShortestPath/Johnson_Tests.cs))
- [X] Travelling salesman problem ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/ShortestPath/TravellingSalesman.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/ShortestPath/TravellingSalesman_Tests.cs))

### Matching

- [X] Max bipartite matching ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/Matching/BiPartiteMatching.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/Matching/BiPartiteMatching_Tests.cs)) using Edmonds Karp's improved Ford Fulkerson max flow algorithm 
- [X] Max bipartite matching ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/Matching/HopcroftKarp.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/Matching/HopcroftKarp_Tests.cs)) using Hopcroft Karp algorithm

### Cut

- [X] Minimum cut ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/Cut/MinimumCut.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/Cut/MinCut_Tests.cs)) using Edmonds Karp's improved Ford Fulkerson max flow algorithm

### Search

- [X] Depth first ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/Search/DepthFirst.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/Search/DepthFirst_Tests.cs))
- [X] Breadth first ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/Search/BreadthFirst.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/Search/BreadthFirst_Tests.cs))
- [X] Bi-directional ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/Search/BiDirectional.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/Search/BiDirectional_Tests.cs))

### Topological sort

- [X] Depth first method ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/Sort/DepthFirstTopSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/Sort/DepthFirstTopSort_Tests.cs))
- [X] Kahn's algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/Sort/KahnTopSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/Sort/KahnTopSort_Tests.cs))

### Minimum spanning tree

- [X] Kruskal's algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/MinimumSpanningTree/Kruskals.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/MinimumSpanningTree/Kruskals_Test.cs)) using merge sort and disjoint set
- [X] Prim's algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Graph/MinimumSpanningTree/Prims.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Graph/MinimumSpanningTree/Prims_Test.cs))

## String

- [X] Manacher's algorithm for linear time longest palindrome ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/String/ManachersPalindrome.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/String/Manachers_Tests.cs))

### Pattern matching

- [X] Rabin-Karp string search ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/String/Search/RabinKarp.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/String/Search/RabinKarp_Tests.cs))
- [X] Knuth–Morris–Pratt (KMP) string search ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/String/Search/KMP.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/String/Search/KMP_Tests.cs))
- [X] Z algorithm for string search ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/String/Search/ZAlgorithm.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/String/Search/Z_Tests.cs))

## Compression

- [X] Huffman coding ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Compression/HuffmanCoding.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Compression/HuffmanCoding_Tests.cs)) 

## Sorting and searching

- [X] Binary search ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Search/BinarySearch.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Search/BinarySearch_Tests.cs))
- [X] Quick select for kth smallest/largest in unordered collection using median of medians ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Search/QuickSelect.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Search/QuickSelect_Tests.cs))
- [X] Majority element using Boyer-Moore voting algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Search/BoyerMoore.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Search/BoyerMoore_Tests.cs))

### Sorting algorithms

- [X] Bubble sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Sorting/BubbleSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Sorting/BubbleSort_Tests.cs))
- [X] Insertion sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Sorting/InsertionSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Sorting/InsertionSort_Tests.cs))
- [X] Selection sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Sorting/SelectionSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Sorting/SelectionSort_Tests.cs))
- [X] Shell sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Sorting/ShellSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Sorting/ShellSort_Tests.cs))
- [X] Tree sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Sorting/TreeSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Sorting/TreeSort_Tests.cs))
- [X] Quick sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Sorting/QuickSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Sorting/QuickSort_Tests.cs))
- [X] Heap sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Sorting/HeapSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Sorting/HeapSort_Tests.cs))
- [X] Merge sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Sorting/MergeSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Sorting/MergeSort_Tests.cs))
- [X] Bucket sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Sorting/BucketSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Sorting/BucketSort_Tests.cs))
- [X] Radix sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Sorting/RadixSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Sorting/RadixSort_Tests.cs))
- [X] Counting sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Sorting/CountingSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Sorting/CountingSort_Tests.cs))

Note: On a decent desktop, in given implementations here for +ive random input integers, the clear winner is counting sort (~0.1 seconds to sort 1 million integers) followed by Radix Sort (~0.4 seconds). Merge Sort, Heap Sort, Quick Sort & Bucket Sort are all winners for +ive & -ive random integer inputs. Tree sort has pointer juggling overhead on backing Red-Black Tree, so performs 8 times slower than Merge Sort in practice. Bubble Sort, Insertion Sort, Selection Sort & Shell Sort are among the worst for random input as observed from results.

## Combinatorics

- [X] Permutations ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Combinatorics/Permutation.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Combinatorics/Permutation_Tests.cs))
- [X] Combinations ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Combinatorics/Combination.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Combinatorics/Combination_Tests.cs))
- [X] Subsets ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Combinatorics/Subset.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Combinatorics/Subset_Tests.cs))

## Distributed Systems

- [X] Circular queue (ring buffer) ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Distributed/CircularQueue.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Distributed/CircularQueue_Tests.cs))
- [X] Consistant hash ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Distributed/ConsistentHash.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Distributed/ConsistentHash_Tests.cs))
- [X] LRU cache ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Distributed/LRUCache.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Distributed/LRUCache_Tests.cs))
- [X] Asynchronous producer–consumer queue ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Distributed/AsyncQueue.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Distributed/AsyncQueue_Tests.cs))

## Numerical methods

- [X] Check primality ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Numerical/PrimeTester.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Numerical/Primality_Tests.cs))
- [X] Generate primes using sieve of Eratosthenes ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Numerical/PrimeGenerator.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Numerical/PrimeGenerator_Tests.cs))
- [X] Fast exponentiation ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Numerical/Exponentiation.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Numerical/Exponentiation_Tests.cs))

## Geometry (in 2D)

- [X] Convex hull using gift wrapping algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Geometry/ConvexHull.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Geometry/ConvexHull_Tests.cs))
- [X] Line intersection ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Geometry/LineIntersection.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Geometry/LineIntersection_Tests.cs))
- [X] Closest point pair ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Geometry/ClosestPointPair.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Geometry/ClosestPointPair_Tests.cs))
- [X] Check if given point inside polygon ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Geometry/PointInsidePolygon.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Geometry/PointInsidePolygon_Tests.cs))
- [X] Rectangle intersection ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Geometry/RectangleIntersection.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Geometry/RectangleIntersection_Tests.cs))
- [X] Point rotation ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Geometry/PointRotation.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Geometry/PointRotation_Tests.cs))
- [X] Line interesections with Bentley-Ottmann sweep line algorithm using red-black tree and binary minimum heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Geometry/BentleyOttmann.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Geometry/BentleyOttmann_Tests.cs))

## Bit manipulation

- [X] Base conversion ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Binary/BaseConversion.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Binary/BaseConversion_Tests.cs))
- [X] Calculate logarithm (base 2 & 10) ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Binary/Logarithm.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Binary/Logarithm_Tests.cs))
- [X] GCD ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/Binary/GCD.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/tests/Advanced.Algorithms.Tests/Binary/GCD_Tests.cs))

