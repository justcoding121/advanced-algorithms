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

- [X] Array list (dynamic array) ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/List/ArrayList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Lists/ArrayList_Tests.cs))
- [X] Skip list ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/List/SkipList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Lists/SkipList_Tests.cs))

### HashSets

- [X] HashSet (using [separate chaining](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/HashSet/SeparateChainingHashSet.cs) optionally with [open address linear probing](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/HashSet/OpenAddressHashSet.cs)) ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/HashSet/HashSet.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/HashSet/HashSet_tests.cs))
- [X] Sorted HashSet ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/HashSet/SortedHashSet.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/HashSet/SortedHashSet_tests.cs))

### Dictionaries

- [X] Dictionary (using [separate chaining](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Dictionary/SeparateChainingDictionary.cs) optionally with [open address linear probing](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Dictionary/OpenAddressDictionary.cs)) ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Dictionary/Dictionary.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Dictionary/Dictionary_tests.cs))
- [X] Sorted Dictionary ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Dictionary/SortedDictionary.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Dictionary/SortedDictionary_tests.cs))

### Stack

- [X] Stack (using [dynamic array](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Stack/ArrayStack.cs) and optionally using [singly linked list](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Stack/LinkedListStack.cs)) ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Stack/Stack.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Stack_tests.cs))

### Queue

- [X] Queue (using [dynamic array](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Queues/ArrayQueue.cs) and optionally using [doubly linked list](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Queues/LinkedListQueue.cs)) ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Queues/Queue.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Queues/Queue_tests.cs))

#### Priority queue

- [X] Min priority queue ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Queues/PriorityQueue/MinPriorityQueue.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Queues/PriorityQueue/MinPriorityQueue_tests.cs))
- [X] Max priority queue ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Queues/PriorityQueue/MaxPriorityQueue.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Queues/PriorityQueue/MaxPriorityQueue_tests.cs))
 
### Linked list

- [X] Singly linked list ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/LinkedList/SinglyLinkedList.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/LinkedList/SinglyLinkedList_tests.cs))
- [X] Doubly linked list ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/LinkedList/DoublyLinkedList.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/LinkedList/DoublyLinkedList_tests.cs))
- [X] Circular linked list ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/LinkedList/CircularLinkedList.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/LinkedList/CircularLinkedList_tests.cs))

### Heap

#### Min

- [X] Binary min heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Min/BMinHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Heap/Min/BMinHeap_tests.cs))
- [X] d-ary min heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Min/d-aryMinHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Heap/Min/D-aryMinHeap_tests.cs))
- [X] Binomial min heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Min/BinomialMinHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Heap/Min/BinomialMinHeap_tests.cs))
- [X] Fibornacci min Hheap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Min/FibornacciMinHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Heap/Min/FibornacciMinHeap_tests.cs))
- [X] Pairing min heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Min/PairingMinHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Heap/Min/PairingMinHeap_tests.cs))

#### Max 

- [X] Binary max heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Max/BMaxHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Heap/Max/BMaxHeap_tests.cs))
- [X] d-ary max heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Max/d-aryMaxHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Heap/Max/D-aryMaxHeap_tests.cs))
- [X] Binomial max heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Max/BinomialMaxHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Heap/Max/BinomialMaxHeap_tests.cs))
- [X] Fibornacci max heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Max/FibornacciMaxHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Heap/Max/FibornacciMaxHeap_tests.cs))
- [X] Pairing max heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Max/PairingMaxHeap.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Heap/Max/PairingMaxHeap_tests.cs))

Note: It is observed that among the implementations here in practice, with the exclusion of DecrementKey/IncrementKey operation regular Binary Heap & d-ary Heap outperforms other in theory superiors. Likely because it don't have pointer juggling overhead and hey arrays are faster!

### Tree

- [X] Tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/Tree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/Tree_tests.cs))
- [X] Binary tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/BinaryTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/BinaryTree_tests.cs))

#### Binary search trees

- [X] Binary search tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/BST.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/BST_tests.cs))
- [X] AVL tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/AvlTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/AVLTree_tests.cs))
- [X] Red black tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/RedBlackTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/RedBlackTree_tests.cs))
- [X] Splay tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/SplayTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/SplayTree_tests.cs))
- [X] Treap tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/TreapTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/TreapTree_tests.cs))

#### B trees (database trees)

- [X] B-tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/BTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/BTree_tests.cs))
- [X] B+ tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/B%2BTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/B%2BTree_tests.cs))

#### Queryable trees

- [X] Segment tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/SegmentTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/SegmentTree_tests.cs))
- [X] Binary indexed tree (Fenwick tree) ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/FenwickTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/FenwickTree_tests.cs))
- [X] Multi-dimensional interval tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/IntervalTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/IntervalTree_tests.cs)) using nested Red-black tree
- [X] Multi-dimensional k-d tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/K_DTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/KdTree_tests.cs)) for range and nearest neigbour queries
- [X] Multi-dimensional range tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/RangeTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/RangeTreetests.cs)) using nested Red-black tree
- [X] R-tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/RTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/RTree_tests.cs))
- [X] Quadtree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/QuadTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/QuadTree_tests.cs))

TODO: Support multi-dimentional segment tree & binary indexed tree.

#### Lookup trees

- [X] Prefix tree (Trie) ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/Trie.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/Trie_tests.cs))
- [X] Suffix tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/SuffixTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/SuffixTree_tests.cs))
- [X] Ternary search tree ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/TernarySearchTree.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Tree/TernarySearchTree_tests.cs))

TODO: implement trie compression.

#### Set

- [X] Disjoint set ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Set/DisJointSet.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Set/DisJointSet_tests.cs))
- [X] Sparse set ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Set/SparseSet.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Set/SparseSet_tests.cs))
- [X] Bloom filter ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Set/BloomFilter.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Set/BloomFilter_tests.cs))

### Graph

#### Adjacency list

- [X] Graph ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Graph/AdjacencyList/Graph.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Graph/AdjacencyList/Graph_tests.cs))
- [X] Weighted Graph ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Graph/AdjacencyList/WeightedGraph.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Graph/AdjacencyList/WeightedGraph_tests.cs))
- [X] DiGraph ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Graph/AdjacencyList/DiGraph.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Graph/AdjacencyList/DiGraph_tests.cs))
- [X] Weighted DiGraph ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Graph/AdjacencyList/WeightedDiGraph.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Graph/AdjacencyList/WeightedDiGraph_tests.cs))

#### Adjacency matrix

- [X] Graph ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Graph/AdjacencyMatrix/Graph.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Graph/AdjacencyMatrix/Graph_tests.cs))
- [X] Weighted Graph ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Graph/AdjacencyMatrix/WeightedGraph.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Graph/AdjacencyMatrix/WeightedGraph_tests.cs))
- [X] DiGraph ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Graph/AdjacencyMatrix/DiGraph.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Graph/AdjacencyMatrix/DiGraph_tests.cs))
- [X] Weighted DiGraph ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Graph/AdjacencyMatrix/WeightedDiGraph.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/DataStructures/Graph/AdjacencyMatrix/WeightedDiGraph_tests.cs))


# Algorithms

## Graph algorithms

### Articulation points

- [X] Tarjan's articulation points finder ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/ArticulationPoint/TarjansArticulationFinder.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/ArticulationPoints/TarjansArticulation_tests.cs))

### Bridges

- [X] Tarjan's bridge finder ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Bridge/TarjansBridgeFinder.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/Bridge/TarjansBridge_tests.cs))

### Connectivity

- [X] Kosaraju's strongly connected component finder ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Connectivity/KosarajuStronglyConnected.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/Connectivity/KosarajuStronglyConnected_tests.cs))
- [X] Tarjan's strongly connected component finder ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Connectivity/TarjansStronglyConnected.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/Connectivity/TarjansStronglyConnected_tests.cs))
- [X] Tarjan's bi-connected graph tester ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Connectivity/TarjansBiConnected.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/Connectivity/TarjansBiConnected_tests.cs))

### Coloring

- [X] M-coloring ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Coloring/MColorer.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/Coloring/MColoring_tests.cs))

### Cover

- [X] Min vertex cover ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Cover/MinVertexCover.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/Cover/MinVertexCover.cs))

### Maximum flow

- [X] Ford-Fulkerson algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Flow/FordFulkerson.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/Flow/FordFulkerson_tests.cs))
- [X] Edmonds Karp's improvement ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Flow/EdmondsKarp.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/Flow/EdmondsKarp_tests.cs)) on Ford-Fulkerson algorithm
- [X] Push relabel algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Flow/PushRelabel.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/Flow/PushRelabel_tests.cs))

### Shortest path

- [X] Bellman-Ford algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/ShortestPath/Bellman-Ford.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/ShortestPath/BellmanFord_tests.cs))
- [X] Dijikstra's algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/ShortestPath/Dijikstra.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/ShortestPath/Dijikstras_tests.cs)) using Fibornacci Heap.
- [X] Floyd-Warshall algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/ShortestPath/Floyd-Warshall.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/ShortestPath/FloydWarshall_tests.cs))
- [X] Johnson's algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/ShortestPath/Johnsons.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/ShortestPath/Johnson_tests.cs))
- [X] Travelling salesman problem ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/ShortestPath/TravellingSalesman.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/ShortestPath/TravellingSalesman_tests.cs))

### Matching

- [X] Max bipartite matching ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Matching/BiPartiteMatching.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/Matching/BiPartiteMatching_tests.cs)) using Edmonds Karp's improved Ford Fulkerson Max Flow algorithm 
- [X] Max bipartite matching ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Matching/HopcroftKarp.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/Matching/HopcroftKarp_tests.cs)) using Hopcroft Karp algorithm

### Cut

- [X] Minimum cut ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Cut/MinimumCut.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/Cut/MinCut_tests.cs)) using Edmonds Karp's improved Ford Fulkerson Max Flow algorithm

### Search

- [X] Depth first ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Search/DepthFirst.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/Search/DepthFirst_tests.cs))
- [X] Breadth first ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Search/BreadthFirst.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/Search/BreadthFirst_tests.cs))
- [X] Bi-directional ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Search/BiDirectional.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/Search/BiDirectional_tests.cs))

### Topological sort

- [X] Depth first method ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Sort/DepthFirstTopSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/Sort/DepthFirstTopSort_tests.cs))
- [X] Kahn's algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Sort/KahnTopSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/Sort/KahnTopSort_tests.cs))

### Minimum spanning tree

- [X] Kruskal's algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/MinimumSpanningTree/Kruskals.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/MinimumSpanningTree/Kruskals_Test.cs)) using Merge Sort and Disjoint Set
- [X] Prim's algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/MinimumSpanningTree/Prims.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Graph/MinimumSpanningTree/Prims_Test.cs))

## String

- [X] Manacher's algorithm for linear time longest Palindrome ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/String/ManachersPalindrome.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/String/Manachers_tests.cs))

### Pattern matching

- [X] Rabin-Karp string search ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/String/Search/RabinKarp.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/String/Search/RabinKarp_tests.cs))
- [X] Knuth–Morris–Pratt (KMP) string search ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/String/Search/KMP.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/String/Search/KMP_tests.cs))
- [X] Z algorithm for string search ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/String/Search/ZAlgorithm.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/String/Search/Z_tests.cs))

## Compression

- [X] Huffman coding ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Compression/HuffmanCoding.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Compression/HuffmanCoding_tests.cs)) 

## Sorting and searching

- [X] Binary search ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Search/BinarySearch.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Search/BinarySearch_tests.cs))
- [X] Quick select for kth smallest/largest in unordered collection using median of medians ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Search/QuickSelect.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Search/QuickSelect_tests.cs))
- [X] Majority element using Boyer-Moore voting algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Search/BoyerMoore.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Search/BoyerMoore_tests.cs))

### Sorting algorithms

- [X] Bubble sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/BubbleSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Sorting/BubbleSort_tests.cs))
- [X] Insertion sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/InsertionSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Sorting/InsertionSort_tests.cs))
- [X] Selection sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/SelectionSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Sorting/SelectionSort_tests.cs))
- [X] Shell sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/ShellSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Sorting/ShellSort_tests.cs))
- [X] Tree sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/TreeSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Sorting/TreeSort_tests.cs))
- [X] Quick sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/QuickSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Sorting/QuickSort_tests.cs))
- [X] Heap sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/HeapSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Sorting/HeapSort_tests.cs))
- [X] Merge sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/MergeSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Sorting/MergeSort_tests.cs))
- [X] Bucket sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/BucketSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Sorting/BucketSort_tests.cs))
- [X] Radix sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/RadixSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Sorting/RadixSort_tests.cs))
- [X] Counting sort ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/CountingSort.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Sorting/CountingSort_tests.cs))

Note: On a decent desktop, in given implementations here for +ive random input integers, the clear winner is counting sort (~0.1 seconds to sort 1 million integers) followed by Radix Sort (~0.4 seconds). Merge Sort, Heap Sort, Quick Sort & Bucket Sort are all winners for +ive & -ive random integer inputs. Tree sort has pointer juggling overhead on backing Red-Black Tree, so performs 8 times slower than Merge Sort in practice. Bubble Sort, Insertion Sort, Selection Sort & Shell Sort are among the worst for random input as observed from results.

## Combinatorics

- [X] Permutations ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Combinatorics/Permutation.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Combinatorics/Permutation_tests.cs))
- [X] Combinations ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Combinatorics/Combination.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Combinatorics/Combination_tests.cs))
- [X] Subsets ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Combinatorics/Subset.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Combinatorics/Subset_tests.cs))

## Distributed Systems

- [X] Circular queue (ring buffer) ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Distributed/CircularQueue.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Distributed/CircularQueue_tests.cs))
- [X] Consistant hash ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Distributed/ConsistentHash.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Distributed/ConsistentHash_tests.cs))
- [X] LRU cache ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Distributed/LRUCache.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Distributed/LRUCache_tests.cs))
- [ ] Asynchronous producer–consumer queue

## Numerical methods

- [X] Check primality ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Numerical/PrimeTester.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Numerical/Primality_tests.cs))
- [X] Generate primes using sieve of Eratosthenes ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Numerical/PrimeGenerator.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Numerical/PrimeGenerator_tests.cs))
- [X] Fast exponentiation ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Numerical/Exponentiation.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Numerical/Exponentiation_tests.cs))

## Geometry (in 2D)

- [X] Convex hull using gift wrapping algorithm ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Geometry/ConvexHull.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Geometry/ConvexHull_tests.cs))
- [X] Line intersection ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Geometry/LineIntersection.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Geometry/LineIntersection_tests.cs))
- [X] Closest point pair ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Geometry/ClosestPointPair.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Geometry/ClosestPointPair_tests.cs))
- [X] Check if given point inside polygon ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Geometry/PointInsidePolygon.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Geometry/PointInsidePolygon_tests.cs))
- [X] Rectangle intersection ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Geometry/RectangleIntersection.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Geometry/RectangleIntersection_tests.cs))
- [X] Point rotation ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Geometry/PointRotation.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Geometry/PointRotation_tests.cs))
- [X] Line interesections with Bentley-Ottmann sweep line algorithm using red-black tree and binary minimum heap ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Geometry/BentleyOttmann.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Geometry/BentleyOttmann_tests.cs))

## Bit manipulation

- [X] Base conversion ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Binary/BaseConversion.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Binary/BaseConversion_tests.cs))
- [X] Calculate logarithm (base 2 & 10) ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Binary/Logarithm.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Binary/Logarithm_tests.cs))
- [X] GCD ([implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Binary/GCD.cs) | [tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.tests/Binary/GCD_tests.cs))

