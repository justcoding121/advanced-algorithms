### Note: 

Please don't take effort to create pull requests for new algorithms/data structures. This is just a curiosity driven personal hobby and [was originally not intented to be a library](https://github.com/justcoding121/Advanced-Algorithms/issues/2). Feel free fork and modify to fit your need if that's what you are looking for. You can however open issues/fix bugs with pull requests here, I would be happy to take a look when I get time.

# Advanced Algorithms

Various important computer science algorithms generically implemented in C#.

<a href="https://ci.appveyor.com/project/justcoding121/advanced-algorithms">![Build Status](https://ci.appveyor.com/api/projects/status/9xpcp4m87max2066?svg=true)</a>

Install by [nuget](https://www.nuget.org/packages/Advanced.Algorithms)

For beta releases on [beta branch](https://github.com/justcoding121/Advanced-Algorithms/tree/beta)

    Install-Package Advanced.Algorithms -Pre

Not a stable release yet.

* [API Documentation](https://justcoding121.github.io/Advanced-Algorithms/api/Advanced.Algorithms.DataStructures.html)

Supports

 * .Net Standard 1.0 or above
 * .Net Framework 4.0 or above
 
## Data structures

### List

- [X] Array list (dynamic array) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/List/ArrayList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Lists/ArrayList_Tests.cs))
- [X] Skip list ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/List/SkipList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Lists/SkipList_Tests.cs))

### HashSets

- [X] HashSet (using [Separate Chaining](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/HashSet/SeparateChainingHashSet.cs) optionally [Open Address Linear Probing](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/HashSet/OpenAddressHashSet.cs)) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/HashSet/HashSet.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/HashSet/HashSet_Tests.cs))
- [X] Sorted HashSet ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/HashSet/SortedHashSet.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/HashSet/SortedHashSet_Tests.cs))

### Dictionaries

- [X] Dictionary (using [Separate Chaining](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Dictionary/SeparateChainingDictionary.cs) optionally [Open Address Linear Probing](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Dictionary/OpenAddressDictionary.cs)) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Dictionary/Dictionary.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Dictionary/Dictionary_Tests.cs))
- [X] Sorted Dictionary ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Dictionary/SortedDictionary.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Dictionary/SortedDictionary_Tests.cs))

### Stack

- [X] Stack (using [Dynamic Array](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Stack/ArrayStack.cs) and optionally using [Singly Linked List](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Stack/LinkedListStack.cs)) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Stack/Stack.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Stack_Tests.cs))

### Queue

- [X] Queue (using [Dynamic Array](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Queues/ArrayQueue.cs) and optionally using [Doubly Linked List](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Queues/LinkedListQueue.cs)) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Queues/Queue.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Queues/Queue_Tests.cs))

#### Priority queue

- [X] Min priority queue ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Queues/PriorityQueue/MinPriorityQueue.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Queues/PriorityQueue/MinPriorityQueue_Tests.cs))
- [X] Max priority queue ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Queues/PriorityQueue/MaxPriorityQueue.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Queues/PriorityQueue/MaxPriorityQueue_Tests.cs))
 
### Linked list

- [X] Singly linked list ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/LinkedList/SinglyLinkedList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/LinkedList/SinglyLinkedList_Tests.cs))
- [X] Doubly linked list ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/LinkedList/DoublyLinkedList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/LinkedList/DoublyLinkedList_Tests.cs))
- [X] Circular linked list ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/LinkedList/CircularLinkedList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/LinkedList/CircularLinkedList_Tests.cs))

### Heap

#### Min

- [X] Binary min heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Min/BMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Heap/Min/BMinHeap_Tests.cs))
- [X] d-ary min heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Min/d-aryMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Heap/Min/D-aryMinHeap_Tests.cs))
- [X] Binomial min heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Min/BinomialMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Heap/Min/BinomialMinHeap_Tests.cs))
- [X] Fibornacci min Hheap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Min/FibornacciMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Heap/Min/FibornacciMinHeap_Tests.cs))
- [X] Pairing min heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Min/PairingMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Heap/Min/PairingMinHeap_Tests.cs))

#### Max 

- [X] Binary max heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Max/BMaxHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Heap/Max/BMaxHeap_Tests.cs))
- [X] d-ary max heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Max/d-aryMaxHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Heap/Max/D-aryMaxHeap_Tests.cs))
- [X] Binomial max heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Max/BinomialMaxHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Heap/Max/BinomialMaxHeap_Tests.cs))
- [X] Fibornacci max heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Max/FibornacciMaxHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Heap/Max/FibornacciMaxHeap_Tests.cs))
- [X] Pairing max heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Heap/Max/PairingMaxHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Heap/Max/PairingMaxHeap_Tests.cs))

Note: It is observed that among the implementations here in practice, with the exclusion of DecrementKey/IncrementKey operation regular Binary Heap & d-ary Heap outperforms other in theory superiors. Likely because it don't have pointer juggling overhead and hey arrays are faster!

### Tree

- [X] Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/Tree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/Tree_Tests.cs))
- [X] Binary tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/BinaryTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/BinaryTree_Tests.cs))

#### Binary search trees

- [X] Binary search tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/BST.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/BST_Tests.cs))
- [X] AVL tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/AvlTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/AVLTree_Tests.cs))
- [X] Red black tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/RedBlackTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/RedBlackTree_Tests.cs))
- [X] Splay tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/SplayTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/SplayTree_Tests.cs))
- [X] Treap tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/TreapTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/TreapTree_Tests.cs))

#### B trees (database trees)

- [X] B-tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/BTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/BTree_Tests.cs))
- [X] B+ tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/B%2BTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/B%2BTree_Tests.cs))

#### Queryable trees

- [X] Segment tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/SegmentTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/SegmentTree_Tests.cs))
- [X] Binary indexed tree (Fenwick tree) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/FenwickTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/FenwickTree_Tests.cs))
- [X] Multi-dimensional interval tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/IntervalTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/IntervalTree_Tests.cs)) using nested Red-black tree
- [X] Multi-dimensional k-d tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/K_DTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/KdTree_Tests.cs)) for range and nearest neigbour queries
- [X] Multi-dimensional range tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/RangeTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/RangeTreeTests.cs)) using nested Red-black tree
- [X] R-tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/RTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/RTree_Tests.cs))
- [X] Quadtree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/QuadTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/QuadTree_Tests.cs))

TODO: Support multi-dimentional segment tree & binary indexed tree.

#### Lookup trees

- [X] Prefix tree (Trie) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/Trie.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/Trie_Tests.cs))
- [X] Suffix tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/SuffixTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/SuffixTree_Tests.cs))
- [X] Ternary search tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Tree/TernarySearchTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Tree/TernarySearchTree_Tests.cs))

TODO: implement trie compression.

#### Set

- [X] Disjoint set ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Set/DisJointSet.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Set/DisJointSet_Tests.cs))
- [X] Sparse set ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Set/SparseSet.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Set/SparseSet_Tests.cs))
- [X] Bloom filter ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Set/BloomFilter.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Set/BloomFilter_Tests.cs))

### Graph

#### Adjacency list

- [X] Graph ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Graph/AdjacencyList/Graph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyList/Graph_Tests.cs))
- [X] Weighted Graph ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Graph/AdjacencyList/WeightedGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyList/WeightedGraph_Tests.cs))
- [X] DiGraph ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Graph/AdjacencyList/DiGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyList/DiGraph_Tests.cs))
- [X] Weighted DiGraph ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Graph/AdjacencyList/WeightedDiGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyList/WeightedDiGraph_Tests.cs))

#### Adjacency matrix

- [X] Graph ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Graph/AdjacencyMatrix/Graph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyMatrix/Graph_Tests.cs))
- [X] Weighted Graph ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Graph/AdjacencyMatrix/WeightedGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyMatrix/WeightedGraph_Tests.cs))
- [X] DiGraph ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Graph/AdjacencyMatrix/DiGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyMatrix/DiGraph_Tests.cs))
- [X] Weighted DiGraph ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/DataStructures/Graph/AdjacencyMatrix/WeightedDiGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyMatrix/WeightedDiGraph_Tests.cs))


# Algorithms

## Graph algorithms

### Articulation points

- [X] Tarjan's articulation points finder ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/ArticulationPoint/TarjansArticulationFinder.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/ArticulationPoints/TarjansArticulation_Tests.cs))

### Bridges

- [X] Tarjan's bridge finder ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Bridge/TarjansBridgeFinder.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/Bridge/TarjansBridge_Tests.cs))

### Connectivity

- [X] Kosaraju's strongly connected component finder ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Connectivity/KosarajuStronglyConnected.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/Connectivity/KosarajuStronglyConnected_Tests.cs))
- [X] Tarjan's strongly connected component finder ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Connectivity/TarjansStronglyConnected.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/Connectivity/TarjansStronglyConnected_Tests.cs))
- [X] Tarjan's bi-connected graph tester ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Connectivity/TarjansBiConnected.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/Connectivity/TarjansBiConnected_Tests.cs))

### Coloring

- [X] M-coloring ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Coloring/MColorer.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/Coloring/MColoring_Tests.cs))

### Cover

- [X] Min vertex cover ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Cover/MinVertexCover.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/Cover/MinVertexCover.cs))

### Maximum flow

- [X] Ford-Fulkerson algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Flow/FordFulkerson.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/Flow/FordFulkerson_Tests.cs))
- [X] Edmonds Karp's improvement ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Flow/EdmondsKarp.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/Flow/EdmondsKarp_Tests.cs)) on Ford-Fulkerson algorithm
- [X] Push relabel algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Flow/PushRelabel.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/Flow/PushRelabel_Tests.cs))

### Shortest path

- [X] Bellman-Ford algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/ShortestPath/Bellman-Ford.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/ShortestPath/BellmanFord_Tests.cs))
- [X] Dijikstra's algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/ShortestPath/Dijikstra.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/ShortestPath/Dijikstras_Tests.cs)) using Fibornacci Heap.
- [X] Floyd-Warshall algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/ShortestPath/Floyd-Warshall.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/ShortestPath/FloydWarshall_Tests.cs))
- [X] Johnson's algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/ShortestPath/Johnsons.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/ShortestPath/Johnson_Tests.cs))
- [X] Travelling salesman problem ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/ShortestPath/TravellingSalesman.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/ShortestPath/TravellingSalesman_Tests.cs))

### Matching

- [X] Max bipartite matching ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Matching/BiPartiteMatching.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/Matching/BiPartiteMatching_Tests.cs)) using Edmonds Karp's improved Ford Fulkerson Max Flow algorithm 
- [X] Max bipartite matching ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Matching/HopcroftKarp.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/Matching/HopcroftKarp_Tests.cs)) using Hopcroft Karp algorithm

### Cut

- [X] Minimum cut ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Cut/MinimumCut.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/Cut/MinCut_Tests.cs)) using Edmonds Karp's improved Ford Fulkerson Max Flow algorithm

### Search

- [X] Depth first ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Search/DepthFirst.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/Search/DepthFirst_Tests.cs))
- [X] Breadth first ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Search/BreadthFirst.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/Search/BreadthFirst_Tests.cs))
- [X] Bi-directional ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Search/BiDirectional.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/Search/BiDirectional_Tests.cs))

### Topological sort

- [X] Depth first method ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Sort/DepthFirstTopSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/Sort/DepthFirstTopSort_Tests.cs))
- [X] Kahn's algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/Sort/KahnTopSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/Sort/KahnTopSort_Tests.cs))

### Minimum spanning tree

- [X] Kruskal's algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/MinimumSpanningTree/Kruskals.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/MinimumSpanningTree/Kruskals_Test.cs)) using Merge Sort and Disjoint Set
- [X] Prim's algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Graph/MinimumSpanningTree/Prims.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Graph/MinimumSpanningTree/Prims_Test.cs))

## String

- [X] Manacher's algorithm for linear time longest Palindrome ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/String/ManachersPalindrome.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/String/Manachers_Tests.cs))

### Pattern matching

- [X] Rabin-Karp string search ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/String/Search/RabinKarp.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/String/Search/RabinKarp_Tests.cs))
- [X] Knuth–Morris–Pratt (KMP) string search ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/String/Search/KMP.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/String/Search/KMP_Tests.cs))
- [X] Z algorithm for string search ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/String/Search/ZAlgorithm.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/String/Search/Z_Tests.cs))

## Compression

- [X] Huffman coding ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Compression/HuffmanCoding.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Compression/HuffmanCoding_Tests.cs)) 

## Sorting and searching

- [X] Binary search ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Search/BinarySearch.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Search/BinarySearch_Tests.cs))
- [X] Quick select for kth smallest/largest in unordered collection using median of medians ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Search/QuickSelect.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Search/QuickSelect_Tests.cs))
- [X] Majority element using Boyer-Moore voting algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Search/BoyerMoore.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Search/BoyerMoore_Tests.cs))

### Sorting algorithms

- [X] Bubble sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/BubbleSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Sorting/BubbleSort_Tests.cs))
- [X] Insertion sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/InsertionSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Sorting/InsertionSort_Tests.cs))
- [X] Selection sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/SelectionSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Sorting/SelectionSort_Tests.cs))
- [X] Shell sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/ShellSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Sorting/ShellSort_Tests.cs))
- [X] Tree sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/TreeSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Sorting/TreeSort_Tests.cs))
- [X] Quick sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/QuickSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Sorting/QuickSort_Tests.cs))
- [X] Heap sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/HeapSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Sorting/HeapSort_Tests.cs))
- [X] Merge sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/MergeSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Sorting/MergeSort_Tests.cs))
- [X] Bucket sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/BucketSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Sorting/BucketSort_Tests.cs))
- [X] Radix sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/RadixSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Sorting/RadixSort_Tests.cs))
- [X] Counting sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Sorting/CountingSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Sorting/CountingSort_Tests.cs))

Note: On a decent desktop, in given implementations here for +ive random input integers, the clear winner is counting sort (~0.1 seconds to sort 1 million integers) followed by Radix Sort (~0.4 seconds). Merge Sort, Heap Sort, Quick Sort & Bucket Sort are all winners for +ive & -ive random integer inputs. Tree sort has pointer juggling overhead on backing Red-Black Tree, so performs 8 times slower than Merge Sort in practice. Bubble Sort, Insertion Sort, Selection Sort & Shell Sort are among the worst for random input as observed from results.

## Combinatorics

- [X] Permutations ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Combinatorics/Permutation.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Combinatorics/Permutation_Tests.cs))
- [X] Combinations ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Combinatorics/Combination.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Combinatorics/Combination_Tests.cs))
- [X] Subsets ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Combinatorics/Subset.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Combinatorics/Subset_Tests.cs))

## Distributed Systems

- [X] Circular queue (ring buffer) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Distributed/CircularQueue.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Distributed/CircularQueue_Tests.cs))
- [X] Consistant hash ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Distributed/ConsistentHash.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Distributed/ConsistentHash_Tests.cs))
- [X] LRU cache ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Distributed/LRUCache.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Distributed/LRUCache_Tests.cs))
- [ ] Asynchronous producer–consumer queue

## Numerical methods

- [X] Check primality ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Numerical/PrimeTester.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Numerical/Primality_Tests.cs))
- [X] Generate primes using sieve of Eratosthenes ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Numerical/PrimeGenerator.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Numerical/PrimeGenerator_Tests.cs))
- [X] Fast exponentiation ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Numerical/Exponentiation.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Numerical/Exponentiation_Tests.cs))

## Geometry (in 2D)

- [X] Convex hull using gift wrapping algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Geometry/ConvexHull.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Geometry/ConvexHull_Tests.cs))
- [X] Line intersection ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Geometry/LineIntersection.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Geometry/LineIntersection_Tests.cs))
- [X] Closest point pair ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Geometry/ClosestPointPair.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Geometry/ClosestPointPair_Tests.cs))
- [X] Check if given point inside polygon ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Geometry/PointInsidePolygon.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Geometry/PointInsidePolygon_Tests.cs))
- [X] Rectangle intersection ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Geometry/RectangleIntersection.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Geometry/RectangleIntersection_Tests.cs))
- [X] Point rotation ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Geometry/PointRotation.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Geometry/PointRotation_Tests.cs))
- [X] Line interesections with Bentley-Ottmann sweep line algorithm using red-black tree and binary minimum heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Geometry/BentleyOttmann.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Geometry/BentleyOttmann_Tests.cs))

## Bit manipulation

- [X] Base conversion ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Binary/BaseConversion.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Binary/BaseConversion_Tests.cs))
- [X] Calculate logarithm (base 2 & 10) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Binary/Logarithm.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Binary/Logarithm_Tests.cs))
- [X] GCD ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms/Binary/GCD.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Advanced.Algorithms.Tests/Binary/GCD_Tests.cs))

